using System.Reflection;
using Application;

var builder = WebApplication.CreateBuilder(args);
var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
var application = new Startup(builder, xmlPath, Assembly.GetExecutingAssembly());
application.Start();