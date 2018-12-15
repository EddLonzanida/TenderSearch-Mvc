#Configure site START
Import-Module WebAdministration
  
function IsNull($obj){

    return $null -eq $obj
    
}

function IsFalse($obj){

    return $false -eq $obj -or (IsNull $obj)
    
}
 
function WriteHost2($msg1, $color1, $msg2, $color2){

    Write-Host 
    Write-Host $msg1 -ForegroundColor $color1 -NoNewline

    if(IsNull $color2){

        Write-Host $msg2

    }else{

        Write-Host $msg2 -ForegroundColor $color2

    }
}

function WriteHost3($msg1, $color1, $msg2, $color2, $msg3, $color3, $msg4, $color4){

    Write-Host 
    Write-Host $msg1 -ForegroundColor $color1 -NoNewline

    if(IsNull $color2){

        Write-Host $msg2 -NoNewline

    }else{

        Write-Host $msg2 -ForegroundColor $color2 -NoNewline

    }

    if(IsNull $color3){

        Write-Host $msg3 -NoNewline

    }else{

        Write-Host $msg3 -ForegroundColor $color3 -NoNewline

    }

    if(IsNull $color4){

        Write-Host $msg4

    }else{

        Write-Host $msg4 -ForegroundColor $color4

    }
}

function WriteHost($msg, $color){

    Write-Host

    if(IsNull $color){

        Write-Host $msg

    }else{

        Write-Host $msg -ForegroundColor $color

    }
}

function DisplayPadRight($pad, $msg1, $msg2){
    
    Write-Host

    $scrWidth = 80
    $color = "Cyan"
    
	if(IsNull $msg2){
    
        Write-Host $msg1.PadRight($scrWidth,$pad) -foreground $color
    
        return
    }
    
    $padLength = $scrWidth - $msg1.Length - 1
    
    Write-Host $msg1 $msg2.PadRight($padLength,$pad) -foreground $color

}

function WriteWarning($msg){
    
    Write-Host

    $color = "Magenta"

    Write-Host $msg -foreground $color
    
}

function DisplayEndingMessage($siteName){
    
    Write-Host
    DisplayPadRight '▲' "*END $siteName"
	Write-Host 
	Write-Host 
}

function CreateHostsEntry($hostName, $ipAdddress){
    Try{
    
	    if((Get-Command "Remove-HostEntry" -ErrorAction SilentlyContinue) -eq $null) {
    
		    if((Get-Command "Install-Module" -ErrorAction SilentlyContinue) -eq $null) {
    
			    (new-object Net.WebClient).DownloadString("http://psget.net/GetPsGet.ps1") | iex
			    Import-Module PsGet
    
		    }
    
		    Install-Module PsHosts
	    }

        WriteHost3 "Creating entry " DarkGray "$ipAdddress $hostName" White " in " DarkGray "C:\Windows\System32\drivers\etc\hosts"

        $siteExists = Get-HostEntry | Where-Object { $_.Name -eq $hostName }
    
        if($siteExists -ne $null)
        {
    
            Remove-HostEntry -Name $hostName
    
        }
    
        Add-HostEntry $hostName $ipAdddress
    
        return $true
    
    }catch{
    
        return $false
    }
}

function IsDatabaseExists($SQLSvr, $DBName){
    
    Try{
    
        [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo") | Out-Null;
                              
        $MySQLObject = New-Object Microsoft.SqlServer.Management.Smo.Server $SQLSvr;
        $serverFullName = $MySQLObject.databases| Where-Object { $_.name -match $DBName } | Select-Object -first 1 | Select Name
    
        if($serverFullName -eq $null -or $serverFullName.Length -eq 0) {return $false}
    
        return $true
    
    }catch{
    
        return $false
    }
}

function IsSiteExists($siteName){ 
    
	if(IsNull $siteName){
    
        WriteWarning 'IsSiteExists: siteName not supplied..'
    
        return  $false
    }
    
    Try{
    
        Set-Location IIS:\Sites
    
        $siteExists = Get-ChildItem | Where-Object { $_.Name -eq $siteName } | Select-Object -first 1 | Select Name
    
        if($siteExists -ne $null) {return $true}
    
        return $false
    
    }catch{
    
        return $false
    }
}

function DeleteSite($siteName){
    
    $siteExists = IsSiteExists $siteName 
    
    if($siteExists -eq $true){ 
    
        Try{
    
            WriteHost2 "Removing Site " DarkGray $siteName
            
            Remove-Website -Name $siteName  
    
        }catch{
    
            return $false
        }
    } 
}

function CreateAppPool($appPoolName){
    
	if(IsNull $appPoolName){
    
        WriteWarning 'CreateAppPool: appPoolName not supplied..'

        return  $false
    
    }
    
    Try{
    
        Set-Location IIS:\AppPools
    
        $appPoolExists = Get-ChildItem –Path IIS:\AppPools | Where { $_.Name -eq "$appPoolName" } | Select-Object -first 1 | Select Name
    
        if($appPoolExists -ne $null){ 
    
            WriteHost2 "Deleting AppPool " DarkGray $appPoolName

            Remove-WebAppPool -Name $appPoolName 
    
        }
    
        WriteHost2 "Creating AppPool " DarkGray $appPoolName
    
        $appPool = New-Item $appPoolName
    
	    return  $true
    
    }catch{
    
        return $false
    }
}

function CreateCertificate($name, $dnsName){
    
	if(IsNull $name){
    
        WriteWarning 'CreateCertificate: name not supplied..'
    
        return  $null
    }
    
	if(IsNull $dnsName){
    
        WriteWarning 'CreateCertificate: dnsName not supplied..'
    
        return  $null
    }

    $cert = Get-ChildItem Cert:\LocalMachine\My | Where-Object { $_.FriendlyName -eq "$name" }| Select-Object -first 1  
    
    if($cert -ne $null){ 
    
        WriteHost2 "Deleting Certificate " DarkGray $name
    
        Get-ChildItem Cert:\LocalMachine\My | Where-Object { $_.FriendlyName -eq "$name" } | Remove-Item

    }
    
    WriteHost2 "Creating Certificate " DarkGray $name
    
    $cert = New-SelfSignedCertificate -FriendlyName $name -CertStoreLocation cert:\LocalMachine\My -DnsName $dnsName #"https://gloffice.services.local.dev:312"#$env:computername

    WriteHost2 "Adding to Trusted Root Certification Authorities store " DarkGray $name

    Get-ChildItem Cert:\LocalMachine\root | Where-Object {$_.Subject -eq "CN=$dnsName"} | Remove-Item
    
    $certStore = New-Object -TypeName System.Security.Cryptography.X509Certificates.X509Store Root, LocalMachine
    
    $certStore.Open("MaxAllowed")
    $certStore.Add($cert)
    $certStore.Close()

    return $cert.GetCertHashString()
}

function CreateSslSite($hostName, $siteName, $physicalPath, $port, $thumbprint){
    
	if(IsNull $hostName){
    
        WriteWarning 'CreateSslSite: hostName not supplied..'
    
        return  $false
    }    
    
	if(IsNull $siteName){
    
        WriteWarning 'CreateSslSite: siteName not supplied..'
    
        return $false
    }
    
	if(IsNull $physicalPath){
    
        WriteWarning 'CreateSslSite: physicalPath not supplied..'
    
        return $false 
    }
    
	if(IsNull $port){
    
        WriteWarning 'CreateSslSite: port not supplied..'
    
        return  $false
    }
    
	if(IsNull $thumbprint){
    
        WriteWarning 'CreateSslSite: thumbprint not supplied..'
    
        return  $false
    }
    

        DeleteSite $siteName

		$localSiteName ="http://${hostName}:$port"
		
        WriteHost3 "Creating Site " DarkGray $localSiteName White " PhysicalPath " DarkGray $physicalPath

        Set-Location IIS:\Sites
        New-Website -Name $siteName -PhysicalPath $physicalPath -IPAddress "*" -Port "$port" -HostHeader $hostName -ApplicationPool $hostName -Ssl  
        Set-WebConfiguration -Location "$siteName" -Filter 'system.webserver/security/access' -Value "Ssl"
        #attach certificate to the website
        Set-Location IIS:\SslBindings

        $sslBindingExists = Get-ChildItem IIS:\SslBindings | Where-Object { $_.Port -eq $port } | Select-Object -first 1  | Select-Object Port

        if($sslBindingExists -ne $null){ 

            WriteHost2 "Removing SslBindings " DarkGray $sslBindingExists

            Remove-Item "0.0.0.0!$port"

        }

        WriteHost 'Attaching SSL Certificate...' Gray

        $webServerCert = Get-ChildItem Cert:\LocalMachine\My\$thumbprint
        $webServerCert | New-Item 0.0.0.0!$port

        WriteHost2 "Starting Site " DarkGray $siteName

        Start-Website -Name $siteName
		
        return $true
}

function CreateWebSite($hostName, $siteName, $physicalPath, $port){
    
	if(IsNull $hostName){
    
        WriteWarning 'CreateSslSite: hostName not supplied..'
    
        return  $false
    }    
    
	if(IsNull $siteName){
    
        WriteWarning 'CreateSslSite: siteName not supplied..'
    
        return $false
    }
    
	if(IsNull $physicalPath){
    
        WriteWarning 'CreateSslSite: physicalPath not supplied..'
    
        return $false 
    }
    
	if(IsNull $port){
    
        WriteWarning 'CreateSslSite: port not supplied..'
    
        return  $false
    }

        DeleteSite $siteName

		$localSiteName ="http://${hostName}:$port"
		
        WriteHost3 "Creating Site " DarkGray $localSiteName White " PhysicalPath " DarkGray $physicalPath

        Set-Location IIS:\Sites
        New-Website -Name $siteName -PhysicalPath $physicalPath -IPAddress "*" -Port "$port" -HostHeader $hostName -ApplicationPool $hostName

        WriteHost2 "Starting Site " DarkGray $siteName

        Start-Website -Name $siteName
		
        return $true
}

function CreateDbUser($loginName, $password, $databaseName, $instanceName){
	#import SQL Server module
    WriteHost3 "Creating DbUser " DarkGray $loginName White " Db " DarkGray $databaseName

    #Write-Host 'Import-Module SQLPS -DisableNameChecking'
	#Import-Module SQLPS -DisableNameChecking 3>$Null #'3' is REDIRECTION OPERATOR for WARNINGS only
    Try{ Import-Module SQLPS -DisableNameChecking 3>$Null }
    Catch{ return  $false }

	if(IsNull $loginName){

        WriteWarning 'CreateDbUser: loginName not supplied..'

        return  $false
    } 

	if(IsNull $password){

        WriteWarning 'CreateDbUser: password not supplied..'

        return  $false
    } 

	if(IsNull $databaseName){

        WriteWarning 'CreateDbUser: databaseName not supplied..'

        return  $false
    } 

	if(IsNull $instanceName){

        WriteWarning 'CreateDbUser: instanceName not supplied..'

        return  $false
    } 

	$dbUserName = $loginName
	$roleName = "db_owner"
	
    $isDatabaseExists = IsDatabaseExists $instanceName $databaseName 

    if($isDatabaseExists -eq $false){

        WriteWarning "CreateDbUser: [$databaseName] db does not exists!"
        WriteWarning "Open .sln -> Right click Web project, set as start-up project -> open Package Manager Console -> In default project dropdown, select .Data.Migration -> run 'update-database -verbose'"
        
        return $false

    }

    $server = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Server -ArgumentList $instanceName
    
    if(IsNull $server){

        WriteWarning "CreateDbUser: unable to locate server [$instanceName]"

        return  $false
    }  

	# drop login if it exists
	$database = $server.Databases[$databaseName]
	
	if ($server.Logins.Contains($loginName))  
	{   

        WriteHost3 "Deleting existing Db login " DarkGray $loginName White " Db " DarkGray $databaseName
        	
        Try{ $server.Logins[$loginName].Drop()  }
        Catch{ return  $false }
        
	}

	$login = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Login -ArgumentList $server, $loginName
	$login.LoginType = [Microsoft.SqlServer.Management.Smo.LoginType]::WindowsUser
    $login.PasswordExpirationEnabled = $false
    
    $login.Create($password)
    
    WriteHost3 "Login " DarkGray $loginName White " created successfully in db " DarkGray $databaseName

    if ($database.Users[$dbUserName])
    {

        WriteHost3 "Dropping user " DarkGray $dbUserName White " from db " DarkGray $databaseName

        Try{ $database.Users[$dbUserName].Drop() }
        Catch{ return  $false }   

    }	
	
	$dbUser = New-Object -TypeName Microsoft.SqlServer.Management.Smo.User -ArgumentList $database, $dbUserName
    $dbUser.Login = $loginName
    $dbUser.Create()

    WriteHost3 "User " DarkGray $dbUser White " created successfully in db " DarkGray $databaseName
	
	$dbrole = $database.Roles[$roleName]
    $dbrole.AddMember($dbUserName)
    $dbrole.Alter()

    WriteHost3 "User " DarkGray $dbUser White " successfully added to role " DarkGray $roleName
	
    return $true
}

function GetPhysicalPath($origDirectory, $relativePath){

    $tmpPath = "$origDirectory\$relativePath"

    #cleanse path
    return $tmpPath -replace "\\{2,20}","\\"
}

function ExitOnError($siteName, $origDirectory){

    DisplayEndingMessage $siteName
    Set-Location $origDirectory
    
    Write-Error "" -ErrorAction Stop
}    

# Paste this file to together with the .sln
function SetupHttp($siteName, $relativePath, $port){

    DisplayPadRight '▼' "*START $siteName"

    $origDirectory = get-item $PSScriptRoot

	if(IsNull $siteName){

        WriteWarning 'siteName not supplied..'

        ExitOnError $siteName $origDirectory

    } 

	if(IsNull $relativePath){

        WriteWarning 'relativePath not supplied..'

        ExitOnError $siteName $origDirectory

    } 

	if(IsNull $port){

        WriteWarning 'port not supplied..'

        ExitOnError $siteName $origDirectory

    } 

    $physicalPath = GetPhysicalPath $origDirectory.FullName $relativePath
    $ipAdddress = "127.0.0.1"
    $hostName = "$siteName.local"
	$loginName = "IIS APPPOOL\$hostName"
	$databaseName = $siteName
	$instanceName = $env:computername
    $certificateDnsName = 'http://' + $hostName + ':' + $port 
    $certificateName = "$siteName Certificate"
    $dbPassword = "Denim_123"	
    
    if(!(Test-Path -Path $physicalPath)){

        WriteWarning "Does not exist: $physicalPath"
	
        ExitOnError $siteName $origDirectory
	
    }

    #create app pool
    $result = CreateAppPool $hostName

    if(IsFalse $result){

        ExitOnError $siteName $origDirectory
        
    }

	#create website
    $result = CreateWebSite $hostName $siteName $physicalPath $port

    if(IsFalse $result){
        
        ExitOnError $siteName $origDirectory
       
    }

	#create entry in C:\Windows\System32\drivers\etc\hosts
    $result = CreateHostsEntry $hostName $ipAdddress 
        
    if(IsFalse $result){
        
        ExitOnError $siteName $origDirectory

    }

    #create db user
    $result = CreateDbUser $loginName $dbPassword $databaseName $instanceName
        
    if(IsFalse $result){
        
        ExitOnError $siteName $origDirectory
       
    }
        
    WriteHost2 "Setup complete " DarkYellow $certificateDnsName Yellow 
    WriteHost "Turn windows features on or off -> Internet Information Services -> World Wide Web Services -> Application Development Features -> All except CGI"
    # chrome://flags/#allow-insecure-localhost 

    DisplayEndingMessage $siteName    
        
	Set-Location $origDirectory
}

Clear-Host
#run!
SetupHttp "TenderSearch" "Hosts\TenderSearch.Web" 313
