using System;
using System.Collections.Generic;
using SportAssoWebApp.Models;
namespace SportAssoWebApp.Views;

public static class FakeDataGenerator
{
    private static Random random = new Random();

    public static List<Creneau> GenerateFakeCreneaux(int numberOfCreneaux)
    {
        var fakeCreneaux = new List<Creneau>();

        for (int i = 0; i < numberOfCreneaux; i++)
        {
            var creneau = new Creneau
            {
                CreneauId = i + 1,
                SectionId = random.Next(1, 5),
                Lieu = "Lieu " + (i + 1),
                Horaire = DateTime.Now.AddHours(random.Next(1, 100)),
                PlacesMax = random.Next(10, 50),
                PlacesRestantes = random.Next(0, 10),
                Price = (decimal)(random.NextDouble() * 50 + 10),
                DocumentsRequired = "Document " + (i + 1)
            };
            fakeCreneaux.Add(creneau);
        }

        return fakeCreneaux;
    }

    public static List<Adherent> GenerateFakeAdherents(int numberOfAdherents)
    {
        var fakeAdherents = new List<Adherent>();

        for (int i = 0; i < numberOfAdherents; i++)
        {
            var adherent = new Adherent
            {
                AdherentId = i + 1,
                FirstName = "FirstName " + (i + 1),
                LastName = "LastName " + (i + 1),
                Email = $"adherent{i + 1}@example.com",
                PasswordHash = "PasswordHash" + (i + 1),
                IsEncadrant = (i % 2 == 0)
            };
            fakeAdherents.Add(adherent);
        }

        return fakeAdherents;
    }

    public static List<Section> GenerateFakeSections(int numberOfSections)
    {
        var fakeSections = new List<Section>();

        for (int i = 0; i < numberOfSections; i++)
        {
            var section = new Section
            {
                SectionId = i + 1,
                SectionName = "Section " + (i + 1)
            };
            fakeSections.Add(section);
        }

        return fakeSections;
    }

    public static List<Inscription> GenerateFakeInscriptions(int numberOfInscriptions, List<Adherent> adherents, List<Creneau> creneaux)
    {
        var fakeInscriptions = new List<Inscription>();

        for (int i = 0; i < numberOfInscriptions; i++)
        {
            var inscription = new Inscription
            {
                InscriptionId = i + 1,
                AdherentId = adherents[random.Next(adherents.Count)].AdherentId,
                CreneauId = creneaux[random.Next(creneaux.Count)].CreneauId,
                IsPaid = (i % 2 == 0),
                DocumentsSubmitted = "Document " + (i + 1)
            };
            fakeInscriptions.Add(inscription);
        }

        return fakeInscriptions;
    }
}

