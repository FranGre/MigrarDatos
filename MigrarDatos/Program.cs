// See https://aka.ms/new-console-template for more information
using MigrarDatos;

string rutaCsv = @"C:\Users\Fran\Downloads\customersW-100.csv";

//var customers = Migracion.LeerCSV(rutaCsv);
Migracion.ModificarCSV(rutaCsv, @"C:\Users\Fran\Downloads\c-100.csv");