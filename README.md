# Cognitive Vision App

This application demonstrates how to use Azure's Computer Vision API with .NET to analyze images and extract descriptions.

## Prerequisites

- .NET SDK installed on your machine. You can download it from [here](https://dotnet.microsoft.com/download).
- Visual Studio Code or any other code editor.
- An Azure subscription. If you don't have one, you can create a free account [here](https://azure.microsoft.com/free/).

## Setup

### 1. Clone the Repository

```sh
git clone https://github.com/your-username/cognitive-vision-app.git
```

```sh
cd cognitive-vision-app
```


### 2. Install the Required Packages
Open a terminal in the project directory and run:

```sh
dotnet add package Microsoft.Azure.CognitiveServices.Vision.ComputerVision
```

### 3. Create a Cognitive Services Resource on Azure
- Go to the [Azure Portal](https://portal.azure.com/#home).
- Click on "Create a resource" and search for "Cognitive Services".
- Select "Computer Vision" and click "Create".
- Fill in the necessary details and create the resource.
- Once created, navigate to the resource and copy the API key and endpoint URL

### 4. Configure the Application
Open `Program.cs` and replace `YOUR_SUBSCRIPTION_KEY` and `YOUR_ENDPOINT_URL` with the values you copied from Azure.

```cs
private const string subscriptionKey = "YOUR_SUBSCRIPTION_KEY";
private const string endpoint = "YOUR_ENDPOINT_URL";
```

### 5. Run the Application
In the terminal, run the following command:

```sh
dotnet run
```

## Code Overview
The main functionality of the application is in `Program.cs`. The application uses the Azure Cognitive Services Computer Vision API to analyze an image and return a description.

```cs
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    private const string subscriptionKey = "YOUR_SUBSCRIPTION_KEY";
    private const string endpoint = "YOUR_ENDPOINT_URL";

    static async Task Main(string[] args)
    {
        try
        {
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = endpoint
            };

            // Known working image URL
            string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3a/Cat03.jpg";

            var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Description };
            var result = await client.AnalyzeImageAsync(imageUrl, features);

            Console.WriteLine($"Request ID: {result.RequestId}");
            Console.WriteLine($"Metadata: {result.Metadata}");
            foreach (var caption in result.Description.Captions)
            {
                Console.WriteLine($"Description: {caption.Text} with confidence {caption.Confidence}");
            }
        }
        catch (ComputerVisionErrorResponseException e)
        {
            Console.WriteLine($"Computer Vision API error: {e.Message}");
            if (e.Body != null && e.Body.Error != null)
            {
                Console.WriteLine($"Error Code: {e.Body.Error.Code}");
                Console.WriteLine($"Error Message: {e.Body.Error.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }
    }
}
```

## Troubleshooting
If you encounter any issues, please check the following:

- Ensure your API key and endpoint URL are correct.
- Verify the image URL is publicly accessible and in a supported format (JPEG, PNG, BMP, or GIF).
- Check the Azure Cognitive Services pricing and usage limits.

## Resources

- [Azure Cognitive Services Documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/)
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Azure Free Account](https://azure.microsoft.com/free/)

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.
