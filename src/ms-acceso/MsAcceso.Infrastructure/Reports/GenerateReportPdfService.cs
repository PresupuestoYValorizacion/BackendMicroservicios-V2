using MsAcceso.Domain.Root.Reports;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace MsAcceso.Infrastructure.Reports
{
    public record HojaPresupuestoData(
        string codPresupuesto,
        string descPresupuesto,
        string codSubPresupuesto,
        string descSubPresupuesto,
        string cliente, 
        string lugar, 
        string fechaCosto,
        List<Titulo> titulos,
        decimal costoDirecto);

    public record Titulo(
        string item,
        string descripcion,
        decimal parcial,
        List<SubTitulo> subTitulos);

    public record SubTitulo(
        string item,
        string descripcion,
        string unidad,
        string metrado,
        decimal precio,
        decimal parcial);

    public record Partida(
        string descripcion,
        string unidad,
        string cuadrilla,
        string cantidad,
        decimal precio,
        decimal parcial);

    internal sealed class GenerateReportPdfService : IGenerateReportPdfService
    {

        public HojaPresupuestoData GetHojaPresupuesto() => new HojaPresupuestoData(
            codPresupuesto: "HP001",
            descPresupuesto: "CONSTRUCCIÓN DE EDIFICIO COMERCIAL",
            codSubPresupuesto: "SP001",
            descSubPresupuesto: "FUNDACIONES Y ESTRUCTURAS",
            cliente: "EMPRESA CONSTRUCTORA XYZ S.A.",
            lugar: "AV. PRINCIPAL 123, CIUDAD",
            fechaCosto: "2024-01-01",
            titulos: GetTitulos(),
            costoDirecto: 16800.00m);

        public List<Titulo> GetTitulos() => new List<Titulo>()
        {
            new Titulo(
                "001",
                "CONSTRUCCIÓN DE CIMIENTOS",
                // Unidad y metrado están comentados en el modelo de Titulo
                // Se omiten en esta estructura
                parcial: 8000.00m, // Costo total del título calculado
                subTitulos: new List<SubTitulo>
                {
                    new SubTitulo(
                        "001.01",
                        "EXCAVACIÓN MANUAL",
                        "M3",
                        "15.00",
                        200.00m, // Costo unitario
                        3000.00m  // Costo total (15.00 * 200.00)
                    ),
                    new SubTitulo(
                        "001.02",
                        "COLOCACIÓN DE HORMIGÓN",
                        "M3",
                        "10.00",
                        500.00m, // Costo unitario
                        5000.00m  // Costo total (10.00 * 500.00)
                    )
                }
            ),
            new Titulo(
                "002",
                "ESTRUCTURAS METÁLICAS",
                parcial: 8800.00m, // Costo total del título calculado
                subTitulos: new List<SubTitulo>
                {
                    new SubTitulo(
                        "002.01",
                        "SOLDADURA DE VIGAS PRINCIPALES",
                        "KG",
                        "120.00",
                        15.00m,  // Costo unitario
                        1800.00m  // Costo total (120.00 * 15.00)
                    ),
                    new SubTitulo(
                        "002.02",
                        "INSTALACIÓN DE SOPORTES",
                        "UD",
                        "20.00",
                        350.00m, // Costo unitario
                        7000.00m  // Costo total (20.00 * 350.00)
                    )
                }
            )
        };

        public Document GenerateHojaPresupuestoPdf()
        {
            var hojaPresupuesto = GetHojaPresupuesto();

            var report = Document.Create(container => 
            {

                container.Page(page =>
                {
                    IContainer DefaultCellStyle(IContainer container, int border, string backgroundColor)
                    {
                        return container
                            .Border(border)
                            .BorderColor(Colors.Black)
                            .Background(backgroundColor)
                            .PaddingVertical(2)
                            .AlignMiddle();
                    }

                    page.Margin(20);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(6));

                    page.Header()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(55);
                                columns.ConstantColumn(45);
                                columns.RelativeColumn();
                                columns.ConstantColumn(85);

                            });

                            table.Header(header =>
                            {
                                header.Cell().ColumnSpan(4).ExtendHorizontal().PaddingBottom(5).AlignCenter().Text("Presupuesto").FontSize(10).ExtraBold();

                            });

                            table.Cell().Element(CellHeaderStyle).Text("Presupuesto");
                            table.Cell().Element(CellHeaderStyle).Text(hojaPresupuesto.codPresupuesto).ExtraBold();
                            table.Cell().Element(CellHeaderStyle).Text(hojaPresupuesto.descPresupuesto).ExtraBold();
                            table.Cell();

                            table.Cell().Element(CellHeaderStyle).Text("SubPresupuesto");
                            table.Cell().Element(CellHeaderStyle).Text(hojaPresupuesto.codSubPresupuesto).ExtraBold();
                            table.Cell().Element(CellHeaderStyle).Text(hojaPresupuesto.descSubPresupuesto).ExtraBold();
                            table.Cell();

                            table.Cell().Element(CellHeaderStyle).Text("Cliente");
                            table.Cell().ColumnSpan(2).ExtendHorizontal().Element(CellHeaderStyle).Text(hojaPresupuesto.cliente).ExtraBold();
                            table.Cell().Element(CellHeaderStyle).Text($"Costo al:     {hojaPresupuesto.fechaCosto}").ExtraBold();

                            table.Cell().Element(CellHeaderStyle).Text("Lugar");
                            table.Cell().ColumnSpan(2).ExtendHorizontal().Element(CellHeaderStyle).Text(hojaPresupuesto.lugar).ExtraBold();
                            table.Cell();

                            IContainer CellHeaderStyle(IContainer container) =>
                                                                DefaultCellStyle(container, 0, Colors.White).ShowOnce().AlignLeft();
                        });

                    page.Content()
                        .PaddingVertical(5)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(55);
                                columns.ConstantColumn(325);
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(55);

                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellHeaderStyle).Text("Item").ExtraBold();
                                header.Cell().Element(CellHeaderStyle).Text("Descripción").ExtraBold();
                                header.Cell().Element(CellHeaderStyle).Text("Und").ExtraBold();
                                header.Cell().Element(CellHeaderStyle).Text("Metrado").ExtraBold();
                                header.Cell().Element(CellHeaderStyle).Text("Precio S/.").ExtraBold();
                                header.Cell().Element(CellHeaderStyle).Text("Parcial S/.").ExtraBold();

                                IContainer CellHeaderStyle(IContainer container) =>
                                                                DefaultCellStyle(container, 1, Colors.White).ShowOnce().PaddingLeft(2);
                            });

                            uint contTitulos = 0;

                            foreach (var titulo in hojaPresupuesto.titulos)
                            {
                                contTitulos++;

                                table.Cell().Row(contTitulos).Column(1).Element(CellStyle).AlignLeft().Text(titulo.item);
                                table.Cell().Row(contTitulos).Column(2).Element(CellStyle).AlignLeft().Text(titulo.descripcion).ExtraBold();
                                table.Cell().Row(contTitulos).Column(6).Element(CellStyle).AlignRight().Text(titulo.parcial.ToString()).ExtraBold();

                                foreach (var subtitulo in titulo.subTitulos)
                                {
                                    contTitulos++;

                                    table.Cell().Element(CellStyle).Text(subtitulo.item);
                                    table.Cell().Element(CellStyle).Text(subtitulo.descripcion);
                                    table.Cell().Element(CellStyle).Text(subtitulo.unidad);
                                    table.Cell().Element(CellStyle).AlignRight().Text(subtitulo.metrado);
                                    table.Cell().Element(CellStyle).AlignRight().Text(subtitulo.precio.ToString("F2"));
                                    table.Cell().Element(CellStyle).AlignRight().Text(subtitulo.parcial.ToString("F2"));
                                }

                            }

                            contTitulos++;

                            table.Cell().Row(contTitulos).Column(2).Element(CellStyle).Text("COSTO DIRECTO").ExtraBold(); 
                            table.Cell().Row(contTitulos).Column(6).AlignRight().Element(CellStyle).Text(hojaPresupuesto.costoDirecto.ToString()).ExtraBold();
                            
                            contTitulos++;

                            var costoDirecto = NumeroAPalabras(hojaPresupuesto.costoDirecto);

                            table.Cell().Row(contTitulos).Column(2).Element(CellStyle).Text($"SON:    {costoDirecto}   NUEVOS SOLES").ExtraBold();

                            IContainer CellStyle(IContainer container) =>
                                                                DefaultCellStyle(container, 0, Colors.White).ShowOnce().PaddingLeft(2).AlignLeft();
                        });

                    var fechaActual = GetFechaActual();
                    page.Footer().AlignRight().Text($"Fecha:   {fechaActual}");
                });
            });

            report.ShowInCompanion();

            return report;
        }

        static string NumeroAPalabras(decimal number)
        {
            if (number == 0)
                return "CERO";

            if (number < 0)
                return "MENOS " + NumeroAPalabras(Math.Abs(number));

            // Separar parte entera y parte decimal
            long integerPart = (long)Math.Floor(number); // Parte entera
            int decimalPart = (int)((number - integerPart) * 100); // Parte decimal hasta dos cifras

            string words = ConvertirEnteroANumeros(integerPart);

            if (decimalPart > 0)
            {
                words += " CON " + ConvertirEnteroANumeros(decimalPart) + " CENTÉSIMOS";
            }

            return words.Trim().ToUpper(); // Convertir a mayúsculas
        }

        static string ConvertirEnteroANumeros(long number)
        {
            string[] unitsMap = { "", "UNO", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE" };
            string[] teensMap = { "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISÉIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE" };
            string[] tensMap = { "", "", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };
            string[] hundredsMap = { "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" };

            var words = "";

            if (number >= 1000000)
            {
                long millions = number / 1000000;
                words += ConvertirEnteroANumeros(millions) + " MILLÓN" + (millions > 1 ? "ES " : " ");
                number %= 1000000;
            }

            if (number >= 1000)
            {
                long thousands = number / 1000;
                if (thousands == 1)
                    words += "MIL ";
                else
                    words += ConvertirEnteroANumeros(thousands) + " MIL ";
                number %= 1000;
            }

            if (number >= 100)
            {
                long hundreds = number / 100;
                if (hundreds == 1 && number % 100 == 0)
                    words += "CIEN ";
                else
                    words += hundredsMap[hundreds] + " ";
                number %= 100;
            }

            if (number >= 20)
            {
                long tens = number / 10;
                words += tensMap[tens];
                number %= 10;

                if (number > 0)
                    words += " Y ";
            }
            else if (number >= 10)
            {
                words += teensMap[number - 10];
                number = 0;
            }

            if (number > 0)
            {
                words += unitsMap[number];
            }

            return words.Trim();
        }

        static string GetFechaActual()
        {
            // Obtener la fecha y hora actual
            DateTime now = DateTime.Now;

            // Formatear la fecha y hora
            string formattedDate = now.ToString("dd/MM/yyyy hh:mm:sstt", CultureInfo.InvariantCulture);

            // Reemplazar AM/PM por a.m./p.m.
            formattedDate = formattedDate.Replace("AM", "a.m.").Replace("PM", "p.m.");

            return formattedDate;
        }
    }
}
