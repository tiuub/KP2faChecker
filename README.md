# KP2faChecker
KP2faChecker is a plugin for [KeePass](http://keepass.info). It provides a column and a form to display whether or not they support 2FA.

[![Latest Release](https://img.shields.io/github/v/release/tiuub/KP2faChecker)](https://github.com/tiuub/KP2faChecker/releases/latest)
[![GitHub All Releases](https://img.shields.io/github/downloads/tiuub/KP2faChecker/total)](https://github.com/tiuub/KP2faChecker/releases/latest)
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5F5QB7744AD5G&source=url)

## Installation

 - Download the latest release [here](https://github.com/tiuub/KP2faChecker/releases/latest)
 - Copy the KP2faChecker.plgx in the KeePass plugins directory and restart the application.


### Alternative Installation
- Using [chocolatey](https://chocolatey.org/) in Powershell with `choco install keepass-plugin-kp2fachecker`



## Usage

### First Method

At first you have to activate the column. Therefore navigate to
 - View -> Configure Columns -> (Scroll down) -> Check "2FA Support"


If you have activated, it should show you the column.

![Column](Screenshots/screenshot-1.PNG)

Now you can douple click the column to get additional informations.

![Additional informations](Screenshots/screenshot-2.PNG)


### Second Method

Just right click any entry in your database and click on "Check for 2fa support". 

![Context Menu](Screenshots/screenshot-3.PNG)

This will open another window with additional informations.

![Additional informations](Screenshots/screenshot-2.PNG)


### Third Method

You can search your wanted website by name or domain. Therefore navigate to
- Tools -> KP2faChecker - Search Websites -> Search query.

![Search query](Screenshots/screenshot-4.PNG)



## Download

You can download the .plgx file [here](https://github.com/tiuub/KP2faChecker/releases/latest).


## Additional Information

I have build my own API [here](https://toasted.top/kp2fac/api/v1/get/all.php), which receives its data from [2fa.directory (formerly twofactorauth.org)](https://2fa.directory/). 
To save resources and also because 2fa.directory isnt changing its data that often, the plugin will only request every 2 days for new added websites. 



## Data privacy

The plugin is requesting a huge json file from a server and compares the domains inside this json locally on your computer. This means your private data will never be sent through the internet.



## License

[![GitHub](https://img.shields.io/github/license/tiuub/KP2faChecker)](https://github.com/tiuub/KP2faChecker/blob/master/LICENSE)
