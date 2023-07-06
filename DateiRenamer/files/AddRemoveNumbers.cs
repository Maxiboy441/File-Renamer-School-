using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace files
{
    class AddRemoveNumbers
    {

        public void AddRemoveNumber(string pfad)
        {
            
           /* if (!Directory.Exists(pfad))
            {
                Directory.CreateDirectory(pfad);
            }*/

            string[] dateien = Directory.GetFiles(pfad); //speichert eine Liste von Dateinamen aus dem angegebenen Verzeichnis in der Variable "dateien".
            Array.Sort(dateien);
            // Suffix/Präfix ändern und löschen
            foreach (string datei in dateien) //führt eine Schleife durch alle Elemente in der Liste "dateien" aus
            {
                string dateiname = Path.GetFileNameWithoutExtension(datei); //nimmt den Dateinamen einer Datei und entfernt die Dateierweiterung, um nur den reinen Namen zu erhalten.
                string neueErweiterung = ".png"; // Gibt neue Name 
                                                 // Prüfen, ob der Dateiname mit "Clipboard" beginnt
                if (dateiname.StartsWith("Clipboard"))
                {
                    // Ersetzen von "Clipboard" durch "img-" und Hinzufügen der neuen Erweiterung
                    string neuerName = dateiname.Replace("Clipboard", "img-") + neueErweiterung;
                    File.Move(datei, Path.Combine(pfad, neuerName)); //verschiebt eine Datei an einen neuen Speicherort und gibt ihr einen neuen Namen. Dabei wird der ursprüngliche Dateipfad "datei" mit dem neuen Speicherort "pfad" und dem neuen Dateinamen "neuerName" kombiniert.
                }
                else
                {
                    // Ersetzen von "Renamer" durch "art-" und Hinzufügen der neuen Erweiterung
                    string neuerName = dateiname.Replace("Renamer", "art-") + neueErweiterung;
                    File.Move(datei, Path.Combine(pfad, neuerName));
                }
            }

            // Teilausdrücke umbenennen
            foreach (string datei in dateien)
            {
                string dateiname = Path.GetFileNameWithoutExtension(datei);
                // Prüfen, ob der Dateiname mit "Renamer" beginnt
                if (dateiname.StartsWith("Renamer"))
                {
                    // Ersetzen von "art-1" durch "art-a", "art-2" durch "art-b" usw.
                    string neuerName = dateiname.Replace("art-1", "art-a")
                                                .Replace("art-2", "art-b")
                                                .Replace("art-3", "art-c");
                    neuerName += Path.GetExtension(datei);
                    File.Move(datei, Path.Combine(pfad, neuerName));
                }
            }

            // Zahlenblock verschieben
            foreach (string datei2 in dateien)
            {
                // Den Dateinamen ohne Erweiterung holen
                string neuerName = Path.GetFileNameWithoutExtension(datei2);

                // Überprüfen, ob der Dateiname mit "Renamer" beginnt und die Länge mindestens 7 Zeichen beträgt
                if (neuerName.StartsWith("Renamer") && neuerName.Length >= 7)
                {
                    // Den Zahlenblock extrahieren
                    string zahlenblockString = neuerName.Substring(4, 3);

                    // Überprüfen, ob der Zahlenblock eine gültige Zahl ist
                    if (int.TryParse(zahlenblockString, out int zahlenblock))
                    {
                        // Den Zahlenblock um 10 erhöhen
                        zahlenblock += 10;

                        // Den Zahlenblock im Dateinamen aktualisieren
                        neuerName = neuerName.Substring(0, 4) + zahlenblock.ToString("D3") + Path.GetExtension(datei2);

                        // Datei umbenennen
                        File.Move(datei2, Path.Combine(pfad, neuerName));
                    }
                }
            }

            // Führende Nullen einführen / entfernen
            foreach (string datei in dateien)
            {
                string dateiname = Path.GetFileNameWithoutExtension(datei);
                // Prüfen, ob der Dateiname mit "1-" beginnt
                if (dateiname.StartsWith("1-"))
                {
                    // Führende Nullen einführen, indem Leerzeichen mit Nullen aufgefüllt werden
                    string neuerName = dateiname.Substring(3).PadLeft(3, '0'); // die ersten 3 zahlen werden ubersprungen dann wird mit null ersetzt 
                                                                               // "1-" am Anfang des Dateinamens hinzufügen
                    neuerName = neuerName.Insert(0, "1-");
                    neuerName += Path.GetExtension(datei);
                    File.Move(datei, Path.Combine(pfad, neuerName));
                }
                // Prüfen, ob der Dateiname mit "001-" beginnt
                else if (dateiname.StartsWith("001-"))
                {
                    // Führende Nullen entfernen, indem Leerzeichen links abgeschnitten werden
                    string neuerName = dateiname.Substring(4).TrimStart('0');
                    // "1-" am Anfang des Dateinamens hinzufügen
                    neuerName = neuerName.Insert(0, "1-");
                    neuerName += Path.GetExtension(datei);
                    File.Move(datei, Path.Combine(pfad, neuerName));
                }
            }

            // Zahlen neu nummerieren
            for (int i = 0; i < dateien.Length; i++)
            {
                string datei = dateien[i];
                string dateiname = Path.GetFileNameWithoutExtension(datei);

                // Überprüfen, ob der Dateiname mit "Renamer" beginnt und ein "-" enthält
                if (dateiname.StartsWith("Renamer") && dateiname.Contains("-"))
                {
                    // Aufteilen des Dateinamens in zwei Teile anhand des "-"
                    string[] parts = dateiname.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int oldNumber))
                    {
                        // Generieren des neuen Dateinamens mit der neuen Nummer und dem zweiten Teil des Dateinamens
                        string newNumber = (oldNumber - 2).ToString(); // Hier wird die Nummerierung angepasst (9 -> 7)
                        string newFileName = newNumber + "-" + parts[1] + Path.GetExtension(datei);

                        // Umbenennen der Datei
                        File.Move(datei, Path.Combine(pfad, newFileName));
                    }
                }
            }

            // Zahlenblock einfügen / entfernen
            foreach (string datei in dateien)
            {
                string dateiname = Path.GetFileNameWithoutExtension(datei);

                // Überprüfen, ob der Dateiname mit "Renamer" beginnt
                if (dateiname.StartsWith("Renamer"))
                {
                    // Überprüfen, ob der Dateiname einen Zahlenblock enthält
                    if (dateiname.Contains("-"))
                    {
                        // Aufteilen des Dateinamens in zwei Teile anhand des "-"
                        string[] parts = dateiname.Split('-');
                        if (parts.Length == 2)
                        {
                            // Prüfen, ob der erste Teil des Dateinamens eine Zahl ist
                            int zahl;
                            if (int.TryParse(parts[0], out zahl))
                            {
                                // Generieren des neuen Dateinamens mit/ohne Zahlenblock
                                string newFileName = parts[1] + Path.GetExtension(datei);

                                // Umbenennen der Datei
                                File.Move(datei, Path.Combine(pfad, newFileName));
                            }
                        }
                    }
                    else
                    {
                        // Generieren des neuen Dateinamens mit Zahlenblock
                        string newFileName = "1-" + dateiname + Path.GetExtension(datei);

                        // Umbenennen der Datei
                        File.Move(datei, Path.Combine(pfad, newFileName));
                    }
                }
            }

            Console.WriteLine("Die Dateien wurden erfolgreich bearbeitet!");
            Console.ReadLine();
        }

    }


}


