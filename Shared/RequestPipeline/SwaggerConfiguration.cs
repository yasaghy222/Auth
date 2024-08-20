namespace Auth.Shared.RequestPipeline
{
    public static class SwaggerConfiguration
    {
        public static WebApplication AddSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "Auth v2"));
            }

            return app;
        }
    }
}