using System.Data;
using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;

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

        //#7241
        var discountTableName = nameof(Discount);
        var vendorIdDiscountColumnName = nameof(Discount.VendorId);

        if (!Schema.Table(discountTableName).Column(vendorIdDiscountColumnName).Exists())
        {
            Alter.Table(discountTableName)
                .AddColumn(vendorIdDiscountColumnName).AsInt32().ForeignKey<Vendor>(onDelete: Rule.SetNull).Nullable();
        }
    }
}
