﻿using Newtonsoft.Json;
using RecommendationEngineClient.Common.DTO;
using System.Net.Sockets;

namespace RecommendationEngineClient._20_ClientOperations
{
    public abstract class BaseClientOperations
    {
        protected RequestServices _requestServices;

        public BaseClientOperations(RequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        protected async Task<TResponse> SendRequestAsync<TResponse>(string controller, string action, object data = null)
            where TResponse : BaseResponseDTO
        {
            var requestData = new DataObject()
            {
                Controller = controller,
                Action = action,
                Data = data == null ? null : JsonConvert.SerializeObject(data)
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var jsonResponse = await _requestServices.SendRequestAsync(jsonRequest);

            if (jsonResponse == null || string.IsNullOrEmpty(jsonResponse))
                throw new SocketException((int)SocketError.HostDown);

            return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
        }

        protected void PrintBaseResponse(BaseResponseDTO response)
        {
            Console.WriteLine($"Status: {response.Status}, Message: {response.Message}\n");
        }

      
    }
}
