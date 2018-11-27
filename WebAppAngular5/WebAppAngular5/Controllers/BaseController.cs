using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using WebAppAngular5.Base;
using WebAppAngular5.Models;

namespace WebAppAngular5.Controllers
{
    public class BaseController : DefaultControllerFactory
    {
        readonly IRepository _readOnlyRepository;

        public BaseController()
        {
            _readOnlyRepository = _readOnlyRepository ?? new Repository();
        }
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controllerType = GetControllerType(requestContext, controllerName);
            return (IController)Activator.CreateInstance(controllerType, BindingFlags.CreateInstance, null, new object[]
            {
                _readOnlyRepository
            }, null);
        }
    }
}