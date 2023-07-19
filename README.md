# dotnet-wasm-rename-icu

Change extension of ICU files in .NET on WASM app

## Building source code

- Install .NET 8 SDK (preview)
- Install wasm-tools workload `dotnet workload install wasm-tools`
- Run the project `dotnet run`

## Keep eye on

The solution consists of two parts

### MSBuild

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    ...

    <!-- 👇 New extension for ICU files -->
    <NewIcuFileExtension>.icu</NewIcuFileExtension>
  </PropertyGroup>

  ...

  <!-- 👇 After the application is build/published -->
  <Target Name="RenameIcuToAppBundle" AfterTargets="WasmBuildApp;WasmNestedPublishApp">
    <ItemGroup>
      <!-- 👇 Find all ICU files in AppBundle -->
      <IcuFiles Include="$(WasmAppDir)\**\*.dat" />
    </ItemGroup>

    <!-- 👇 Change their extension to 'NewIcuFileExtension' -->
    <Move SourceFiles="@(IcuFiles)"
      OverwriteReadOnlyFiles="true"
      DestinationFiles="%(RelativeDir)%(Filename)$(NewIcuFileExtension)" />
  </Target>
</Project>
```

### Javascript

```js
... = await dotnet
    // 👇 Override defaults. This 'withStartupOptions' functions will go away before GA in favor of 'withResourceLoader' with the same signature as 'loadBootResource'
    .withStartupOptions({
        // 👇 Override resource URL resolution
        loadBootResource: function (type, name, defaultUri, integrity) {
            // 👇 Override extension of ICU files
            if (type == 'globalization') {
                defaultUri = defaultUri.replace('.dat', '.icu');
            }

            return defaultUri;
        }
    })
    .create();
```
