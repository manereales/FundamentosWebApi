namespace AutoresApplication2.Servicios
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "Archivo 1.txt";
        private Timer _timer;

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

           // _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Dispose();
            Escribir("Proceso Finalizado");
            return Task.CompletedTask;
        }

        //private void DoWork(object state)
        //{
        //    Escribir("Proceso en ejecucion: " + DateTime.Now.ToString("dd/mm/yyyy  hh:mm:ss"));
        //}

        private void Escribir(string mensaje)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            { 
                writer.WriteLine(mensaje);
            }
        }
    }
}
