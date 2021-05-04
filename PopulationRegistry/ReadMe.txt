Additional points to consider (Implementation isn’t needed):

1. How to support the same application with different languages?

To support different languages I would suggest add Resources folder with resx files of the cultures to be supported.
Add a method like:
private void SetLanguageDictionary()
    {
        switch (Thread.CurrentThread.CurrentCulture.ToString())
        {
            case "nl-NL":
                MyProject.Language.Resources.Culture = new System.Globalization.CultureInfo("nl-NL");
                break;
            case "en-GB":
                MyProject.Language.Resources.Culture = new System.Globalization.CultureInfo("en-GB");
                break;
				.
				.
				.
            default:
                MyProject.Language.Resources.Culture = new System.Globalization.CultureInfo("en-GB");
                break;
        }
       
    }
In XAML fies, add a namespace reference called lang, and use it in labels.

2. How to limit the changes only to authorized operators?

I would suggest to add login panel to users, and manage users rules to different managment operations by different users types (like: admin, editor, viewer...).

3. What will be the performance limitations and how to overcome them? 

The more persons the system containes, the initialization phase increases. In the real world, having milions of people (or even more) it can be much more hard to initialize and maintain. 
For a system like this, it will be much more efficient to save the data in dedicataed DB, so it won't be loaded each time. The only list of persons that is shown in the current page should be retrived from the DB. 
Caching level should be considerd also, since retriving data from DB takes mach more time.

4. What changes will be needed to support several clients that access the same registry? 

If several clients access the same registry, then we should protect writing\updating persons from several places in the same time. sensitive code areas should be locked for prevanting parallel writing. 
