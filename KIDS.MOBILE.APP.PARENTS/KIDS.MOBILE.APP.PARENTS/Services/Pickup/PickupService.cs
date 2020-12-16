using System;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;

namespace KIDS.MOBILE.APP.PARENTS.Services.Pickup
{
    public class PickupService : IPickupService
    {
        private IRequestProvider _requestProvider;
        public PickupService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
    }
}
