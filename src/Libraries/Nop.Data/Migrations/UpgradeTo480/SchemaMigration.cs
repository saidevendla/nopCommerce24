using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;

namespace Nop.Data.Migrations.UpgradeTo480;

[NopSchemaMigration("2024-06-10 00:00:02", "SchemaMigration for 4.80.0")]
public class SchemaMigration : ForwardOnlyMigration
{
    /// <summary>
    /// Collect the UP migration expressions
    /// </summary>
    public override void Up()
    {
        //#7187
        var ptoductTableName = nameof(Product);
        var hasTierPricesColumnName = "HasTierPrices";
        if (Schema.Table(ptoductTableName).Column(hasTierPricesColumnName).Exists())
            Delete.Column(hasTierPricesColumnName).FromTable(ptoductTableName);

        //#7188
        var hasDiscountsAppliedColumnName = "HasDiscountsApplied";
        if (Schema.Table(ptoductTableName).Column(hasDiscountsAppliedColumnName).Exists())
            Delete.Column(hasDiscountsAppliedColumnName).FromTable(ptoductTableName);

        //#7243
        var vendorTableName = nameof(Vendor);
        var pmCustomerIdColumnName = nameof(Vendor.PmCustomerId);
        if (!Schema.Table(vendorTableName).Column(pmCustomerIdColumnName).Exists())
            Alter.Table(vendorTableName).AddColumn(pmCustomerIdColumnName).AsInt32().Nullable();
    }
}
