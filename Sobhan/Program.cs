using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Sobhan
{
    public class Program
    {
        public static void Main(string[] args)
        {//xcxc
            //IUnitOfWork sds = new dbContext();
            //using (StreamReader r = new StreamReader(@"C:\Users\Sobhan\Desktop\Sobhan\Server\Sobhan\Temp\json.json"))
            // {
            //    string json = r.ReadToEnd();
            //    List<CountryCodes> items = JsonConvert.DeserializeObject<List<CountryCodes>>(json);
            //    foreach (var item in items)
            //    {
            //        sds.MarkAsAdded(item);
            //    }

            //    sds.SaveChanges();
            //}


            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>

            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


    }
}