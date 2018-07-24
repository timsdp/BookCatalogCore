using BC.Bootstrap.Services;
using BC.Infrastructure.Interfaces;
using BC.Infrastructure.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace BC.UI.Web.Models.Context
{
    public class DefaultContext : HttpContext, IRequestContext
    {
        private GenericPrincipal prinipical;
        private const string CONNECTION_KEY = "MedicConnectionString";
        private const string ENCRYPT_KEY = "EncryptKey";
        private readonly string applicationPath;

        public DefaultContext(HttpContext requestContext)
            : base()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_KEY].ToString();
            this.Factory = UnitySetup.CreateFactory(this);
            this.DataContext = this.Factory.GetService<IDataContext>(connectionString);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        #region IRequestContext members
        public string Id
        {
            get
            {
                if (this.prinipical == null || this.prinipical.Identity == null)
                {
                    return string.Empty;
                }

                return this.prinipical.Identity.Name;
            }
        }

        //public IUserToken UserToken { get; private set; }

        public IDataContext DataContext { get; private set; }

        public ILogger Logger { get; private set; }

        public IServiceProviderFactory Factory { get; set; }

        public string ApplicationPath
        {
            get
            {
                return this.applicationPath;
            }
        }

        public IPrincipal Principal
        {
            get
            {
                return this.prinipical;
            }
        }

        public override IFeatureCollection Features => throw new NotImplementedException();

        public override HttpRequest Request => throw new NotImplementedException();

        public override HttpResponse Response => throw new NotImplementedException();

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();

        public override AuthenticationManager Authentication => throw new NotImplementedException();

        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion
    }
}
