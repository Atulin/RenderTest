from locust import HttpUser, task, between

# class ForecastApiUser(HttpUser):
#     wait_time = between(1, 5)
#     host = "http://localhost:5123/"
# 
#     @task
#     def get_forecast_json(self):
#         self.client.get("/forecast/json", name="JSON Endpoint")
# 
#     @task
#     def get_forecast_blazor(self):
#         self.client.get("/forecast/blazor", name="Blazor SSR Endpoint")
#         
#     @task
#     def get_forecast_slice(self):
#         self.client.get("/forecast/slice", name="Razor Slices Endpoint")
        
        
class ForecastSSRUser(HttpUser):
    wait_time = between(1, 5)
    host = "http://localhost:5258/"    
            
    @task
    def get_forecast_pages(self):
        self.client.get("/", name="Razor Pages")

    @task
    def get_forecast_slices(self):
        self.client.get("/weather", name="Razor Slices + IA")
    