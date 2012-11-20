How to use this ASP.NET MVC Design Template
=========================================
There are two steps that you must complete to use a Design Template: (1) Copy the template files to your project and (2) Replace all occurrences of the string [YourProjectName] with the name of your ASP.NET MVC Web application project.

After you download and decompress this Design Template, you'll see the folder "DesignTemplate" which contains design content that can be used with a C# ASP.NET MVC project.

Copy each file from the DesignTemplate folder to your ASP.NET MVC project. For example, if the DesignTemplate folder contains a file with the path DesignTemplate\Content\Site.css then you need to copy this file to the exact same location in your ASP.NET MVC application.

In many cases, you need to overwrite an existing file when copying a file from a Design Template to an ASP.NET MVC project. For example, you almost always need to overwrite the Site.css file. Ensure that you back up all files in an existing ASP.NET MVC application before overwriting the files.

After you finish copying all of the files, you need to replace every occurrence of the string [YourProjectName] in your project with the name of your project. For example, if your ASP.NET MVC project is named MvcApplication1 then you need to replace [YourProjectName] with MvcApplication1.To perform the replacements in Visual Studio, select the menu option Edit, Find and Replace, Quick Replace. 


AJAX login
==========
This template uses jQuery to show a login pane without a postback. For now the jQuery code loads the whole /Account/Login page and then filters out the form part. For extend funcionality and performance, implement a PartialLogin action and replace the url in the jQuery code (Site.master).



Visit my blog: http://blog.opo.li


