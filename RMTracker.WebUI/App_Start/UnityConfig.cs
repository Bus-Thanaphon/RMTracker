using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using RMTracker.DataAccess.InMemory;
using RMTracker.DataAccess.SQL;
using System;

using Unity;

namespace RMTracker.WebUI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IRepository<User_Works>, SQLRepository<User_Works>>();
            container.RegisterType<IRepository<Sub_C2B>, SQLRepository<Sub_C2B>>();
            container.RegisterType<IRepository<On_Hold>, SQLRepository<On_Hold>>();
            container.RegisterType<IRepository<Customer>, SQLRepository<Customer>>();
            container.RegisterType<IRepository<s_Lamination>, SQLRepository<s_Lamination>>();
            container.RegisterType<IRepository<s_Cut>, SQLRepository<s_Cut>>();
            container.RegisterType<IRepository<s_Edgebanding>, SQLRepository<s_Edgebanding>>();
            container.RegisterType<IRepository<s_Drill>, SQLRepository<s_Drill>>();
            container.RegisterType<IRepository<s_Painting>, SQLRepository<s_Painting>>();
            container.RegisterType<IRepository<s_Cleaning>, SQLRepository<s_Cleaning>>();
            container.RegisterType<IRepository<s_Packing>, SQLRepository<s_Packing>>();
            container.RegisterType<IRepository<s_QC>, SQLRepository<s_QC>>();
            container.RegisterType<IRepository<s_Pickup>, SQLRepository<s_Pickup>>();
            container.RegisterType<IRepository<UserList>, SQLRepository<UserList>>();
            container.RegisterType<IRepository<MachineList>, SQLRepository<MachineList>>();
            container.RegisterType<IRepository<Departmentuser>, SQLRepository<Departmentuser>>();
            container.RegisterType<IRepository<WorksPause>, SQLRepository<WorksPause>>();
            container.RegisterType<IRepository<WorksDenine>, SQLRepository<WorksDenine>>();
            container.RegisterType<IRepository<SO_PAUSE>, SQLRepository<SO_PAUSE>>();
            container.RegisterType<IRepository<ReasonPause>, SQLRepository<ReasonPause>>();
            container.RegisterType<IRepository<ReasonDenine>, SQLRepository<ReasonDenine>>();
        }
    }
}