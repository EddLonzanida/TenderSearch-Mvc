using Eml.ClassFactory.Contracts;
using Eml.ConfigParser;
using Eml.ControllerBase.Mvc.Configurations;
using Eml.Mef;
using Eml.MefDependencyResolver.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Config;
using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TenderSearch.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Eml.Logger.ILogger Logger { get; private set; }

        public static string ApplicationName { get; private set; }

        public static string ApplicationVersion { get; private set; }

        public static IClassFactory ClassFactory { get; private set; }

        protected void Application_Start()
        {
            //use ApplicationInsights
            ConfigurationItemFactory.Default.Targets.RegisterDefinition(
                "ApplicationInsightsTarget",
                typeof(Microsoft.ApplicationInsights.NLogTarget.ApplicationInsightsTarget)
            );

            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("Application starting");

            try
            {
                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);

                var rootFolder = System.Web.Hosting.HostingEnvironment.MapPath(@"~\bin");

                ClassFactory = Bootstrapper.Init(rootFolder, new[] { "TenderSearch*.dll" });

                var applicationNameConfig = ClassFactory.GetExport<IConfigBase<string, ApplicationNameConfig>>();

                Logger = ClassFactory.GetExport<Eml.Logger.ILogger>();
                ApplicationVersion = GetApplicationVersion();
                ApplicationName = applicationNameConfig.Value;

                DependencyResolver.SetResolver(new MefDependencyResolver(ClassFactory.Container)); // for mvc controllers

                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                logger.Info("Application started");
            }
            catch (Exception e)
            {
                logger.Fatal(e, "A fatal exception was thrown. The application cannot start.");
                throw;
            }
        }

        private string GetControllerName()
        {
            var routeValues = Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var controllerName = GetControllerName();
            var msg = $"{controllerName}: An unhandled exception occurred";
            var exception = Server.GetLastError();

            if (exception == null) return;

            if (Logger == null)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Info(msg);
            }
            else Logger.Log.Error(exception, msg);
        }

        protected void Application_End()
        {
            const string msg = "Application stopping";

            if (Logger == null)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Info(msg);
            }
            else Logger.Log.Info(msg);
        }

        private static string GetApplicationVersion()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var version = executingAssembly.GetName().Version;

            return version != null ? $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}" : string.Empty;
        }

        private readonly EventHandler<ErrorEventArgs> _serializationErrorHandler = (sender, args) =>
        {
            var isHandled = args.ErrorContext.Error.Message.Contains("on 'System.Data.Entity.DynamicProxies.");

            if (isHandled) args.ErrorContext.Handled = true;
        };
    }
}
