using MVCProject.Models.DataBaseProject;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class ProductNodeProvide : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            List<DynamicNode> returnList = new List<DynamicNode>();

            using (var ctx = new InternertMarkertEntitesContainer())
            {
                foreach (var item in ctx.Products)
                {
                    DynamicNode newNode = new DynamicNode();
                    newNode.Title = item.Title;
                    newNode.ParentKey = "Product";
                    newNode.RouteValues.Add("id", item.Id);
                    returnList.Add(newNode);
                }
            }
            return returnList;
        }
    }
}