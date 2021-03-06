﻿using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI.WebControls;
using SiteServer.Utils;

namespace SiteServer.BackgroundPages.Cms
{
	public class PageRelatedFieldMain : BasePageCms
    {
        public Literal ltlFrames;

        public static string GetRedirectUrl(int siteId, int relatedFieldId, int totalLevel)
        {
            return PageUtils.GetCmsUrl(siteId, nameof(PageRelatedFieldMain), new NameValueCollection
            {
                {"RelatedFieldID", relatedFieldId.ToString()},
                {"TotalLevel", totalLevel.ToString()}
            });
        }

		public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

			if (!IsPostBack)
			{
                var relatedFieldId = AuthRequest.GetQueryInt("RelatedFieldID");
                var totalLevel = AuthRequest.GetQueryInt("TotalLevel");
                var cols = "100%";
                if (totalLevel == 2)
                {
                    cols = "50%,50%";
                }
                else if (totalLevel == 3)
                {
                    cols = "33%,33%,33%";
                }
                else if (totalLevel == 4)
                {
                    cols = "25%,25%,25%,25%";
                }
                else if (totalLevel == 5)
                {
                    cols = "20%,20%,20%,20%,20%";
                }
                var builder = new StringBuilder();
                var urlItem = PageRelatedFieldItem.GetRedirectUrl(SiteId, relatedFieldId, 1);
                builder.Append($@"
<frameset framespacing=""0"" border=""false"" cols=""{cols}"" frameborder=""0"" scrolling=""yes"">
	<frame name=""level1"" scrolling=""auto"" marginwidth=""0"" marginheight=""0"" src=""{urlItem}"" >
");

                for (var i = 2; i <= totalLevel; i++)
                {
                    builder.Append($@"
<frame name=""level{i}"" scrolling=""auto"" marginwidth=""0"" marginheight=""0"" src=""../pageBlank.html"">
");
                }

                builder.Append("</frameset>");

                ltlFrames.Text = builder.ToString();
			}
		}
	}
}
