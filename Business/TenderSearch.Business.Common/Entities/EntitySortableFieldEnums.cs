﻿
    //------------------------------------------------------------------------------
// <auto-generated>
//  Date updated:  Saturday, 15 December 2018 7:00 PM
//  Template:  EntitySortableFieldEnums.tt
//  * Enum members are arranged from top to bottom, the same order it was declared.
//  * Or use SortOrder attribute into your property.
// </auto-generated>
//------------------------------------------------------------------------------
namespace TenderSearch.Business.Common.Entities
{
    public enum eAddress
    {
        StreetAddress,
        OwnerType,
        AddressType
    }

    public enum eCityMunicipality
    {
        Name,
        ZipCode
    }

    public enum eCompany
    {
        Name,
        Description,
        Website,
        AbnCan
    }

    public enum eContract
    {
        ContractType,
        DateSigned,
        EndDate,
        RenewalDate,
        Price
    }

    public enum eDependent
    {
        FirstName,
        LastName,
        MiddleName,
        DisplayName,
        BirthDate,
        Gender,
        Relationship,
        CivilStatus
    }

    public enum eEmployee
    {
        FirstName,
        LastName,
        MiddleName,
        DisplayName,
        BirthDate,
        Gender,
        EmployeeNumber,
        AtmNumber,
        SssNumber,
        TinNumber,
        PhilHealthNumber,
        PagIbigNumber,
        PassportNumber,
        DepartmentName,
        MembershipDate,
        HiredDate,
        EmploymentEndDate,
        CivilStatus
    }

    public enum eHasAllOptions
    {
        Name,
        Description
    }

    public enum eHasCustomActionForIndex
    {
        Name,
        Description
    }

    public enum eHasCustomDropDown
    {
        Name,
        Description
    }

    public enum eHasCustomDropDownForEditCreate
    {
        Name,
        Description
    }

    public enum eIsSoftDelete
    {
        Name,
        Description
    }

    public enum eLookup
    {
        Group,
        SubGroup,
        Value,
        Text
    }

}
