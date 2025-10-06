using FluentValidation;
using HotelManagement.Application.Behaviors;
using HotelManagement.Application.Features.Bookings;
using HotelManagement.Application.Interfaces;
using HotelManagement.Application.Validators;
using HotelManagement.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Office.Tools.Excel;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HotelManagement.Presentation
{
    public partial class ThisAddIn
    {
        public IServiceProvider ServiceProvider { get; private set; }
        private Timer roomStatusTimer;
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            var services = new ServiceCollection();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateBookingCommand).Assembly);

                // ValidationBehavior hinzufügen
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // FluentValidation Validators registrieren
            services.AddTransient<IValidator<CreateBookingCommand>, CreateBookingValidator>();
            
            // Repositories
            string bookingFolder = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "HotelBookings");

          
            var xmlRepo = new XmlBookingRepository(bookingFolder);
            var excelRepo = new ExcelBookingRepository(this.Application, xmlRepo);

            
            services.AddSingleton<IBookingRepository>(excelRepo);

            services.AddSingleton<IRoomRepository>(
                new ExcelRoomRepository(this.Application));

            services.AddSingleton<IGuestRepository>(
                new ExcelGuestRepository(this.Application));



            ServiceProvider = services.BuildServiceProvider();
            InitializeRoomStatusTimer();
        }

        private void InitializeRoomStatusTimer()
        {
            roomStatusTimer = new Timer();
            roomStatusTimer.Interval = 60000; 
            roomStatusTimer.Tick += async (s, e) => await UpdateRoomStatuses();
            roomStatusTimer.Start();

            System.Diagnostics.Debug.WriteLine("✓ Room Status Timer gestartet (Update alle 60 Sekunden)");

            _ = UpdateRoomStatuses();
        }

        private async Task UpdateRoomStatuses()
        {
            try
            {
                var mediator = ServiceProvider.GetService(typeof(IMediator)) as IMediator;
                if (mediator == null) return;

                var command = new HotelManagement.Application.Features.Rooms.CheckAndUpdateRoomStatusesCommand();
                var result = await mediator.Send(command);

                System.Diagnostics.Debug.WriteLine($"Auto-Update: {result.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Auto-Update: {ex.Message}");
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            if (roomStatusTimer != null)
            {
                roomStatusTimer.Stop();
                roomStatusTimer.Dispose();
            }
        }

        #region VSTO generated code
        private void InternalStartup()
        {
            this.Startup += new EventHandler(ThisAddIn_Startup);
            this.Shutdown += new EventHandler(ThisAddIn_Shutdown);
        }
        #endregion
    }
}