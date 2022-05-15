using System;
using System.Linq;
using NoCostSite.Utils;

namespace NoCostSite.TypeScript
{
    internal class ControllersWriter
    {
        internal void Write(Controller[] controllers, string output)
        {
            var filesWriter = FilesWriter.Init(output);
            WriteDto(controllers, filesWriter);
            WriteControllers(controllers, filesWriter);
        }

        private void WriteDto(Controller[] controllers, FilesWriter filesWriter)
        {
            var dto = controllers
                .SelectMany(x => x.Actions)
                .SelectMany(x => x.AllTypes)
                .Distinct()
                .ToArray();

            var typesResolver = TypesResolver.Create(dto);

            dto.ForEach(x => WriteDto(x, filesWriter, typesResolver));

            WriteIndex(dto, filesWriter);
        }

        private void WriteIndex(ControllerType[] dto, FilesWriter filesWriter)
        {
            var content = dto
                .Select(x => $@"export {{ type {x.Name} }} from ""./{x.Name}"";")
                .Join("\r\n");

            filesWriter.Write("dto", "index", content);
        }

        private void WriteDto(ControllerType dto, FilesWriter filesWriter, TypesResolver typesResolver)
        {
            var dtoTypes = dto
                .Properties
                .Where(x => typesResolver.IsDto(x.Type))
                .Select(x => x.Type.ResolveType().Name)
                .Distinct()
                .Join(", ");

            var imports = !string.IsNullOrEmpty(dtoTypes)
                ? $@"import {{ {dtoTypes} }} from ""./"""
                : String.Empty;

            var properties = dto
                .Properties
                .Select(x => $"   {x.Name}: {typesResolver.Get(x.Type)}")
                .Join(";\r\n");

            var content = @$"{imports}

export interface {dto.Name} {{
{properties}
}}".Trim();

            filesWriter.Write("dto", dto.Name, content);
        }

        private void WriteControllers(Controller[] controllers, FilesWriter filesWriter)
        {
            controllers.ForEach(x => WriteController(x, filesWriter));
            WriteIndex(controllers, filesWriter);
            WriteApiClient(filesWriter);
        }

        private void WriteController(Controller controller, FilesWriter filesWriter)
        {
            var actions = controller
                .Actions
                .Select(Build)
                .Join(",\r\n\r\n");

            var dtoTypes = controller
                .Actions
                .SelectMany(x => new[] {x.Request, x.Response})
                .Where(x => x != null)
                .Distinct()
                .Select(x => x!.Name)
                .Join(", ");
            
            var imports = !string.IsNullOrEmpty(dtoTypes)
                ? $@"import {{ {dtoTypes} }} from ""./dto"""
                : String.Empty;
            
            var content = @$"{imports}
import {{ ApiClient }} from ""./ApiClient"";

export const {controller.Name} = {{
{actions}
}}".Trim();

            filesWriter.Write("", controller.Name, content);

            string Build(ControllerAction action)
            {
                if (action.Request != null)
                {
                    return
                        $@"    {action.Name}: async (request: {action.Request.Name}): Promise<{action.Response.Name}> => {{
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = ""{controller.Name.Replace("Api", "")}"";
        const action = ""{action.Name}"";

        const url = `${{apiUrl}}?Controller=${{controller}}&Action=${{action}}`;
        return await ApiClient.Current!.send(url, request);
    }}";
                }

                return $@"    {action.Name}: async (): Promise<{action.Response.Name}> => {{
        const apiUrl = ApiClient.Current!.getUrl();
        const controller = ""{controller.Name.Replace("Api", "")}"";
        const action = ""{action.Name}"";

        const url = `${{apiUrl}}?Controller=${{controller}}&Action=${{action}}`;
        return await ApiClient.Current!.send(url);
    }}";
            }
        }

        private void WriteApiClient(FilesWriter filesWriter)
        {
            var content = @"export interface IApiClient {
    send: (url: string, data?: any) => Promise<any>;
    getUrl: () => string;
}

export class ApiClient {
    public static Current?: IApiClient;
}";

            filesWriter.Write("", "ApiClient", content);
        }

        private void WriteIndex(Controller[] controllers, FilesWriter filesWriter)
        {
            var content = controllers
                .Select(x => $@"export {{ {x.Name} }} from ""./{x.Name}"";")
                .Append($@"export {{ ApiClient }} from ""./ApiClient"";")
                .Join("\r\n");

            filesWriter.Write("", "index", content);
        }
    }
}