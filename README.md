# TenderSearch
* Sample MVC App for CRUD operations. Featuring Ef6, KnockoutJs,Ioc/Di (MEF) , UnitTests, and more
* Demo app using [Eml*](https://www.nuget.org/packages?q=EddLonzanida) NuGets.
* Check out [EmlExtensions.vsix](https://marketplace.visualstudio.com/items?itemName=eDuDeTification.EmlExtensions) to automate the creation of controllers, views, seeders, and more!.

## Seed the database
1. Open the solution using Visual Studio 2017, compile and build (don't run yet)
2. Right click TenderSearch.Web project and Set as **startup project**
3. Open Package manager console
4. In the 'Default project' **drop down**, select **TenderSearch.DataMigration** (this is important)
5. In the console, type the command below then press enter to execute. 
```javascript
update-database -verbose
```
## Run the application
1. Press F5 to run

## Quick View
### Landing Page
![](https://github.com/EddLonzanida/TenderSearch-Mvc/blob/master/Docs/Art/LandingPage.png)

### Login
![](https://github.com/EddLonzanida/TenderSearch-Mvc/blob/master/Docs/Art/LandingPage.png)

### Simple Dashboard
![](https://github.com/EddLonzanida/TenderSearch-Mvc/blob/master/Docs/Art/Dashboard.png)

### CRUD Operations
![](https://github.com/EddLonzanida/TenderSearch-Mvc/blob/master/Docs/Art/CrudPage.png)

## That's it.
Cheers!