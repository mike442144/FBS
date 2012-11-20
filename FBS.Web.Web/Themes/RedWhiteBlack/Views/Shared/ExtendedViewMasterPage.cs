using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace [YourProjectName].Views.Shared {
  /// <summary>
  /// Fixes XHTML self closing tags on a ViewMasterPage to be HTML 4.01 Strict compliant and also ensures that "~/" in paths is mapped correctly
  /// </summary>
  public class ExtendedViewMasterPage : ViewMasterPage {
    protected override void Render( HtmlTextWriter writer ) {
      using ( var memoryStream = new MemoryStream( ) ) {
        using ( var streamWriter = new StreamWriter( memoryStream ) ) {
          using ( var htmlTextWriter = new HtmlTextWriter( streamWriter ) ) {
            base.Render( htmlTextWriter );
            htmlTextWriter.Flush( );
            memoryStream.Position = 0;

            TextReader textReader = new StreamReader( memoryStream );
            String output = textReader.ReadToEnd( );
            String newOutput = RewriteOutput( output );
            writer.Write( newOutput );
          }
        }
      }
    }

    private String RewriteOutput( String text ) {
      String rewrittenOutput = ReplaceWithAppPath( text );
      rewrittenOutput = FixXhtmlClosingTags( rewrittenOutput );
      return rewrittenOutput;
    }

    private static String ReplaceWithAppPath( String text ) {
      String applicationPath = HttpContext.Current.Request.ApplicationPath;

      applicationPath = applicationPath.EndsWith( "/" ) ? applicationPath : applicationPath + "/";

      return text.Replace( "~/", applicationPath );
    }

    private static String FixXhtmlClosingTags( String text ) {
      String fixedText = text;
      fixedText = fixedText.Replace( "  />", ">" );
      fixedText = fixedText.Replace( " />", ">" );
      fixedText = fixedText.Replace( "/>", ">" );
      return fixedText;
    }
  }
}
