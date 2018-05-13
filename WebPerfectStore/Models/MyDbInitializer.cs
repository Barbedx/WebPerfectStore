using System.Data.Entity;

namespace WebPerfectStore.Models
{
    internal class MyDbInitializer : DropCreateDatabaseIfModelChanges<Na_project>
    {
        protected override void Seed(Na_project context)
        {
            base.Seed(context); 
        }
    }
}