## Pierres Sweet Shop

a small web app for a bakery. utilizes User Authentication and SQL database interaction.

### by Charlie Weber

## Technologies Used

* _C#_
* _ASP.NET Core MVC_
* _.NET 5_
* _NuGet_
* _Microsoft.EntityFrameworkCore_
* _Dotnet EntityFramework Tool_
* _Microsoft.EntityFrameworkCore.Design_


## Description
This web application will allow users to add and remove Treats and Flavors independantly. they can also associate Treats and flavors based on their wildest dreams.


### Technology Requirements

* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
* A text editor like [VS Code](https://code.visualstudio.com/)

## Setup/Installation Requirements

* _Clone this repository to your desktop_
* _Open in text editor_
* _Create appsettings.json in /TreatStore/ directory_
* _Copy this code into appsettings.json, replacing YOUR_PASSWORD with your MySQL password:_
```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=treat_store;uid=root;pwd=YOUR_PASSWORD;"
  }
}
```
* _open new terminal and run SQL_

        $ mysql -uroot -p{your_password})
* _open MySQL Workbench_
* _In terminal, navigate into SweetTreat.Solution/TreatStore/ and enter the command below to install necessary packages_

        $ dotnet restore
* _enter this command to build the program_

       $ dotnet build
* _enter command to build your database_

        $ dotnet ef database update,
* _check MySql Workbench to make sure the Factory database has been built (named treat_store)_
* _enter the command below to view program in your browser_

        $ dotnet run

  

## Known Bugs

* _NA_

## License
_[GPL](https://opensource.org/licenses/gpl-license)_

## Contact Information

charlesweber@gmail.com
## Photos
BG photo by <a href="https://unsplash.com/@mockaroon?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Mockaroon</a> on <a href="https://unsplash.com/s/photos/treat?utm_source=unsplash&utm_medium=referral&utm_content=creditCopyText">Unsplash</a>
  

  