using System.IO;

namespace System.Web.Routing
{
    public class ImageRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string filename = requestContext.RouteData.Values["filename"] as string;

            if (string.IsNullOrEmpty(filename))
            {
                // return a 404 HttpHandler here
            }
            else
            {
                requestContext.HttpContext.Response.Clear();
                requestContext.HttpContext.Response.ContentType = GetContentType(requestContext.HttpContext.Request.Url.ToString());

                string filepath = requestContext.HttpContext.Server.MapPath("~/Arquivos/"+ filename);

                requestContext.HttpContext.Response.WriteFile(filepath);
                requestContext.HttpContext.Response.End();
            }
            return null;
        }

        private static string GetContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp":
                    return "Image/bmp";
                case ".gif":
                    return "Image/gif";
                case ".jpg":
                    return "Image/jpeg";
                case ".png":
                    return "Image/png";
            }
            return "";
        }
    }
}