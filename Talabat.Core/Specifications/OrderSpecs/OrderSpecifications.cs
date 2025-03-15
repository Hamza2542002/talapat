using System.Linq.Expressions;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(int orderId,string customerEmail)
            : base(O => O.CustomerEmail == customerEmail && orderId == O.Id)
        {
            AddIncludes();
        }
        public OrderSpecifications(string paymentIntentId)
            : base(O => paymentIntentId == O.PaymentIntentId)
        {
        }
        public OrderSpecifications(string customerEmail,OrderSpecsParams specsParams)
            :base(O => O.CustomerEmail ==  customerEmail) 
        {
            AddIncludes();
            AddPagination(specsParams);
            ApplySort(specsParams);
        }

        private void ApplySort(OrderSpecsParams specsParams)
        {
            switch (specsParams.Sort)
            {
                case "dateAsc" : OrderBy = O => O.OrderDate; break;
                case "dateDesc" : OrderByDesc = O => O.OrderDate; break;

                case "subTotalAsc": OrderBy = O => O.SubTotal; break;
                case "subTotalDesc": OrderByDesc = O => O.SubTotal; break;

                default:
                    OrderByDesc = O => O.OrderDate;
                    break;
            }
        }

        private void AddIncludes()
        {
            Includes.Add(O => O.OredrItems);
            Includes.Add(O => O.DeleveryMethod);
        }

        private void AddPagination(OrderSpecsParams specsParams)
        {
            IsPaginationEnabled = true;
            PageIndex = specsParams.PageIndex;
            PageSize = specsParams.PageSize;
        }
    }
}
