```

                            ______           __  _____ __                   
                           / ____/___ ______/ /_/ ___// /_  ____ __________ 
                          / /_  / __ `/ ___/ __/\__ \/ __ \/ __ `/ ___/ __ \
                         / __/ / /_/ / /__/ /_ ___/ / / / / /_/ / /  / /_/ /
                        /_/    \__,_/\___/\__//____/_/ /_/\__,_/_/  / .___/ 
                                                                   /_/

                                   ----- WeFact C# SDK -----

```

WeFact is a base C# SDK aiming to facilitate FlySIP api integration into Belgium-Provider VOIP solutions using FlySIP as an invoicing service.

>[!Important]
>This project was developped as a base lib to other tools and is based on my personnal needs. Feel free to use it as a base for your own work.

## Project Struct

- **CLIENT** : contains all "logical" business layers in the FlySIP api like invoice management etc. All class are using a BaseClient.
- **HTTP** : due to high differences in response based on the business layer, we have create a dedicated HTTP folder containing Responses & Requests objects.
- **MODELS** : base models used in requests & responses like Invoices etc.

---

## Official doc

You can consul the official FlySIP xmlrpc api documentation here : <a href="https://developer.wefact.com/?_locale=nl_NL" target="_blank">OFFICIAL DOCUMENTATION</a>
