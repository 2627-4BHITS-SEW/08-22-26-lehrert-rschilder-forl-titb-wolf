using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Tuerschild
{
    public class Textfield : MonoBehaviour
    {

        const string baseUrl = "http://www.htl-salzburg.ac.at";
        string listUrl = baseUrl + "/lehrerinnen";
        private string targetRoom;

        private async void Start()
        {
            targetRoom = gameObject.name.Replace("Sign-", "");
            TMP_Text textComponent = GetComponent<TMP_Text>();


            textComponent.text = await FetchTeachers();
        }



        private async Task<string> FetchTeachers()
        {
            string baseUrl = "https://www.htl-salzburg.ac.at";
            string listUrl = baseUrl + "/lehrerinnen.html";
            string result = "";

            // LehrerInnen-Liste laden
            UnityWebRequest req = UnityWebRequest.Get(listUrl);
            await req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                result = "Fehler beim Laden der Lehrerliste!";
                return result;
            }

            string html = req.downloadHandler.text;

            // Name + Link extrahieren
            Regex teacherRegex = new Regex(
                "<a[^>]*href=\"(?<link>/lehrerinnen-details/[^\"]+)\"[^>]*>\\s*<span[^>]*>(?<name>[^<]+)</span>",
                RegexOptions.IgnoreCase
            );

            var matches = teacherRegex.Matches(html);

            result = $"Lehrer im Raum {targetRoom}:\n\n";

            foreach (Match m in matches)
            {
                string link = m.Groups["link"].Value;
                string name = m.Groups["name"].Value;

                // Raum aus Link extrahieren: -c-205.html → C205
                Match roomMatch = Regex.Match(
                    link,
                    "-(?<letter>[a-h])-(?<number>[0-9]+)\\.html",
                    RegexOptions.IgnoreCase
                );

                if (!roomMatch.Success)
                    continue;

                string letter = roomMatch.Groups["letter"].Value.ToUpper();
                string number = roomMatch.Groups["number"].Value;
                string room = letter + number;

                if (room != targetRoom)
                    continue;

                // Detailseite laden
                UnityWebRequest detailReq = UnityWebRequest.Get(baseUrl + link);
                await detailReq.SendWebRequest();

                if (detailReq.result != UnityWebRequest.Result.Success)
                    continue;

                string teacherHtml = detailReq.downloadHandler.text;

                // Sprechstunde extrahieren
                Match sprechstundeMatch = Regex.Match(
                    teacherHtml,
                    "Sprechstunde:</div>\\s*<div class=\"value\">\\s*<span class=\"text\">(?<sprechstunde>[^<]+)</span>",
                    RegexOptions.IgnoreCase
                );

                string sprechstunde = sprechstundeMatch.Success
                    ? sprechstundeMatch.Groups["sprechstunde"].Value.Trim()
                    : "Keine Sprechstunde";

                // Ausgabe
                result += $"{name}\n";
                result += $"Raum: {room}\n";
                result += $"Sprechstunde: {sprechstunde}\n";
                result += $"-------------------------\n";
            }


            return result;
        }
    }
}