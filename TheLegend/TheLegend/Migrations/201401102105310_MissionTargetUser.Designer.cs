// <auto-generated />
namespace TheLegend.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    public sealed partial class MissionTargetUser : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(MissionTargetUser));
        
        string IMigrationMetadata.Id
        {
            get { return "201401102105310_MissionTargetUser"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}