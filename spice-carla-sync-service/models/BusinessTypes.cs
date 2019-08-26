using System;
using System.Runtime.Serialization;

namespace SpiceCarlaSync.models
{
    public enum BusinessTypes
    {
        [EnumMember(Value = "Private Corporation")]
        PrivateCorporation = 525840000,

        [EnumMember(Value = "Public Corporation")]
        PublicCorporation = 525840001,

        [EnumMember(Value = "Unlimited Liability Corporation")]
        UnlimitedLiabilityCorporation = 525840002,

        [EnumMember(Value = "Limited Liability Corporation")]
        LimitedLiabilityCorporation = 525840003,

        [EnumMember(Value = "General Partnership")]
        GeneralPartnership = 525840004,

        [EnumMember(Value = "Limited Partnership")]
        LimitedPartnership = 525840005,

        [EnumMember(Value = "Limited Liability Partnership")]
        LimitedLiabilityPartnership = 525840006,

        [EnumMember(Value = "Society")]
        Society = 525840007,

        [EnumMember(Value = "Sole Proprietorship")]
        SoleProprietorship = 525840008,

        [EnumMember(Value = "Indigenous Nation")]
        IndigenousNation = 525840009,

        [EnumMember(Value = "Co-op")]
        Coop = 525840010,

        [EnumMember(Value = "Trust")]
        Trust = 525840011,

        [EnumMember(Value = "Estate")]
        Estate = 525840012,

        [EnumMember(Value = "Local Government")]
        LocalGovernment = 525840013,

        [EnumMember(Value = "University")]
        University = 525840014,

        [EnumMember(Value = "Partnership")]
        Partnership = 525840015
    }
}
