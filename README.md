# Image Snipper

A full-stack (REACT - C# MVC) project which lets you upload pictures from your PC, sends it to the back-end, where a python script using opencv and numpy crops the black borders from the image, and returns the web-hosted cropped image url back to the REACT app to render.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

You will need the following packages to get started

```
Git (https://git-scm.com/downloads)
Python (https://www.python.org/downloads/)
OpenCv (pip install opencv-python)
NumPy (pip install numpy)
Dotnet core (https://www.microsoft.com/net/download)
Node (https://nodejs.org/en/download/)
Yarn (https://yarnpkg.com/lang/en/docs/install/#windows-stable)
```

### Clone this project

Navigate into a directory of your choice and execute command

```
>git clone https://github.com/hariharansubramanian/ImageSnipper.git
```

### Running the Front-end React App

To download all the node module dependencies and start the website, navigate into the 'imagesnipper' directory and run yarn start

```
>cd imagesnipper
>yarn start
```
You can now visit http://localhost:3000/ to view the hosted web-site

### Running the Back-end ASP.Net core MVC app

To download all the nuget packages and start your webserver, navigate into ImageSnipper\ImageSnipper-Backend\ImageSnipper-Backend and run 'dotnet run' from the command line

```
>cd ImageSnipper-Backend\ImageSnipper-Backend
>dotnet run
```
1 - Make sure your React app and MVC web app's are up and running
2 - Navigate to 
Your web-server will now be up and running on http://localhost:51422/ 

The MVC project is configured to use Swagger, and serves it on the default page.

Simply go to http://localhost:51421/ on your browser, and it should auto-navigate you to http://localhost:51421//index.html

You can now interact and view the different API's built into the MVC system

![Alt text](readme-assets/swagger-api-ui.png?raw=true "Swagger-UI")

## Using the app

1 - Make sure your React app and MVC web app's are up and running

2 - Navigate to http://localhost:3000/ 

2 - Simply choose a file from your PC

3 - Hit 'Crop Border'


![Alt text](readme-assets/cropping-in-action.png?raw=true "Front-end")

## Built With

* [Visual Studio Code](https://code.visualstudio.com/) - The web framework used
* [Visual Studio](https://visualstudio.microsoft.com/) - Dependency Management
* [PyCharm](https://www.jetbrains.com/pycharm/) - Used to generate RSS Feeds

## Authors

* **Hariharan Subramanian** - *Full stack developer* - (https://github.com/hariharansubramanian)

## Acknowledgments

* Thank you REACT, for being an awesomely easy framework to pick up on the fly.