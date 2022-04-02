namespace AutoresApplication2.Servicios
{
    public interface IServicio
    {
        Guid ObtenerServicioScoped();
        Guid ObtenerServicioTransient();
        Guid ObtenerServicioSingleton();
        void realizarTarea();

    }

    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        private readonly ServicioTrassient servicioTrassient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;

        public ServicioA(ILogger<ServicioA> logger, ServicioTrassient servicioTrassient,
            ServicioScoped servicioScoped, ServicioSingleton servicioSingleton)
        {
            this.logger = logger;
            this.servicioTrassient = servicioTrassient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
        }

        public Guid ObtenerServicioTransient()
        {
            return servicioTrassient.Guid;
        }
        public Guid ObtenerServicioScoped()
        {
            return servicioScoped.Guid;
        }
        public Guid ObtenerServicioSingleton()
        {
            return servicioSingleton.Guid;
        }

        public void realizarTarea()
        {

        }

    }

    public class ServicioB : IServicio
    {
      

        public Guid ObtenerServicioScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerServicioSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerServicioTransient()
        {
            throw new NotImplementedException();
        }

        public void realizarTarea()
        {

        }

    }

    public class ServicioTrassient
    {
        public Guid Guid = Guid.NewGuid();
    }

    public class ServicioScoped
    {
        public Guid Guid = Guid.NewGuid();
    }

    public class ServicioSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }

}
