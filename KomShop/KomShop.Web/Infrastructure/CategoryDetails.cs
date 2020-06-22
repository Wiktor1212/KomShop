using System.Collections.Generic;

namespace KomShop.Web.Models
{
    public class CategoryDetails
    {
        public List<string> PodzIcons = new List<string>
        {
            "fas fa-database",
            "fas fa-vr-cardboard",
            "fas fa-microchip",
            "fas fa-border-none",
            "fas fa-memory",
            "fas fa-box"
        };
        public List<string> PerIcons = new List<string>
        {
            "fas fa-desktop",
            "fas fa-print",
            "fas fa-network-wired",
            "fas fa-mouse",
            "fas fa-keyboard",
            "fas fa-headphones-alt",
            "fas fa-microphone",
            "fas fa-volume-up"
        };
        public List<string> Podzespoly = new List<string>
        {
            "Dyski twarde HDD i SSD",
            "Karty graficzne",
            "Procesory",
            "Płyty główne",
            "Pamięci RAM",
            "Obudowy komputerowe"
        };
        public List<string> Peryferia = new List<string>
        {
            "Monitory",
            "Drukarki",
            "Urządzenia sieciowe",
            "Myszki",
            "Klawiatury",
            "Słuchawki",
            "Mikrofony",
            "Głośniki"
        };
    }
}