# Attribute Usage in Model Classes

Use these attributes to map models to Dataverse tables and columns.

- [DataverseTable]: Marks the class as representing a Dataverse table (logical name).
- [DataverseColumn]: Maps a property to a specific column.

## Example

[DataverseTable('msdyn_customerasset')]
public class CustomerAssetModel
{
    [DataverseColumn('customerassetid')]
    public Guid Id { get; set; }

    [DataverseColumn('name')]
    public string Name { get; set; }
}

## Notes

- Ensure logical names match exactly (case-insensitive but must be correct strings).
- Keep models focused on shape and mapping; business logic belongs in controllers.
