param(
    [string]$configFilePath = "http.json"
)

# Load the configuration from the JSON file
$config = Get-Content $configFilePath -Raw | ConvertFrom-Json

# Extract variables from config
$inputFile = $config.inputFile
$outputFile = $config.outputFile
$baseUrl = $config.variables | Where-Object { $_.key -eq "baseUrl" } | Select-Object -ExpandProperty value

# Initialize the output content
$outputContent = "@sf.Server_HostAddress = $baseUrl`n"
$outputContent += "@sf.ApiKey = {{sf.ApiKey}}`n`n"

# Read the OpenAPI JSON file
$openApiJson = Get-Content $inputFile -Raw | ConvertFrom-Json

# Function to generate a sample JSON object based on the schema
function Generate-SampleJsonObject {
    param ($schema)

    $sample = @{}
    foreach ($property in $schema.properties.PSObject.Properties) {
        $propertyName = $property.Name
        $propertySchema = $property.Value

        switch ($propertySchema.type) {
            "object" { $sample[$propertyName] = Generate-SampleJsonObject -schema $propertySchema }
            "array"  { $sample[$propertyName] = @(Generate-SampleJsonObject -schema $propertySchema.items) }
            "integer" { $sample[$propertyName] = 0 }
            "number" { $sample[$propertyName] = 0.0 }
            "boolean" { $sample[$propertyName] = $false }
            "string" { $sample[$propertyName] = "<string>" }
            default { $sample[$propertyName] = "<unknown>" }
        }
    }
    return $sample
}

# Initialize global variables for path and query parameters
$globalVariables = @{}

# Function to replace parameters with variables and return the variable name
function Replace-ParametersWithVariables {
    param ($paramName, $defaultValue)

    $variableName = "sf.$paramName"
    if (-not $globalVariables.ContainsKey($variableName)) {
        $globalVariables[$variableName] = $defaultValue
    }
    return "{{" + $variableName + "}}"
}

# Function to extract path variables and query parameters
function ExtractAndSetParameters {
    param ($parameters)

    $paramValues = @{}
    foreach ($param in $parameters) {
        $paramName = $param.name
        $paramType = $param.schema.type
        $defaultValue = switch ($paramType) {
            "integer" { 1 }
            "boolean" { $false }
            "string" { "null" }
            default { "null" }
        }
        $paramValues[$paramName] = Replace-ParametersWithVariables -paramName $defaultValue
    }
    return $paramValues
}

# Iterate over paths in the OpenAPI definition
foreach ($path in $openApiJson.paths.PSObject.Properties) {
    $endpoint = $path.Name
    $methods = $path.Value.PSObject.Properties

    # Write a section header for the endpoint
    $outputContent += "## $endpoint`n`n"

    foreach ($method in $methods) {
        $httpMethod = $method.Name.ToUpper()
        $operation = $method.Value

        # Extract path variables, query parameters, and headers
        $pathParams = @{}
        $queryParams = @{}
        $headers = ""
        if ($operation.parameters) {
            $pathParams = $operation.parameters | Where-Object { $_.in -eq "path" } | ExtractAndSetParameters
            $queryParams = $operation.parameters | Where-Object { $_.in -eq "query" } | ExtractAndSetParameters

            $headerParameters = $operation.parameters | Where-Object { $_.in -eq "header" }
            foreach ($header in $headerParameters) {
                $headers += "$($header.name): {{sf.$($header.name)}}`n"
            }
        }

        # Replace path variables in the endpoint
        foreach ($paramName in $pathParams.Keys) {
            $endpoint = $endpoint -replace "\{$paramName\}", $pathParams[$paramName]
        }

        # Add the request to the output content
        $outputContent += "### $httpMethod $endpoint`n"
        $outputContent += "$httpMethod {{sf.Server_HostAddress}}$endpoint"

        # Add query parameters
        if ($queryParams.Count -gt 0) {
            $queryString = "?" + ($queryParams.Keys | ForEach-Object { "$_=$($queryParams[$_])" }) -join "&"
            $outputContent += "$queryString"
        }
        $outputContent += "`n"

        # Add headers
        $outputContent += $headers

        # Handle different HTTP methods
        if ($httpMethod -in @('POST', 'PUT', 'PATCH')) {
            $outputContent += "Content-Type: application/json`n`n"

            $requestBodySchema = $operation.requestBody.content.'application/json'.schema
            $sampleObject = Generate-SampleJsonObject -schema $requestBodySchema
            $sampleJson = $sampleObject | ConvertTo-Json -Depth 10 -Compress

            $outputContent += "$sampleJson`n"
        }

        $outputContent += "`n"
    }
}

# Add global variables to the top of the content
$globalVariablesContent = "`n# sf.server`n`n"
foreach ($key in $globalVariables.Keys) {
    $globalVariablesContent += "@$key = $($globalVariables[$key])`n"
}

# Prepend global variables to the main output content
$outputContent = $globalVariablesContent + "`n" + $outputContent

# Write the output content to the .http file
Set-Content -Path $outputFile -Value $outputContent

Write-Output "Generated requests file: $outputFile"
