CREATE PROCEDURE [dbo].GetContractDuplicates 
	  @isEdit int = -1 --default to false
	, @companyId int
	, @contractId int
	, @renewalDate Date
	, @endDate Date
AS
BEGIN
	SET NOCOUNT ON;

	IF @isEdit = -1
		BEGIN
			SELECT * 
			FROM Contracts
			WHERE	@companyId = CompanyId AND
					@renewalDate BETWEEN  RenewalDate AND EndDate OR @endDate BETWEEN  RenewalDate AND EndDate
					AND [DateDeleted] IS NULL
			ORDER BY CompanyId, EndDate, ContractType 
		END
	ELSE
		BEGIN
			SELECT * 
			FROM Contracts
			WHERE	@companyId = CompanyId AND @contractId != Id AND
					@renewalDate BETWEEN  RenewalDate AND EndDate OR @endDate BETWEEN  RenewalDate AND EndDate
					AND [DateDeleted] IS NULL
			ORDER BY CompanyId, EndDate, ContractType 
		END
END
