export const testHikes = [
  {
    "id": 1,
    "name": "Mátrahegy",
    "date": "2018-03-09T00:00:00",
    "description": "Mátrahegy telejsítménytúra",
    "website": "http://kekesturista.hu",
    "organizerId": 3,
    "organizer": null,
    "staff": null,
    "comments": null,
    "courses": [
      {
        "id": 1,
        "name": "Mátrahegy 40",
        "hikeId": 1,
        "price": 1000,
        "registerDeadline": "2018-03-08T00:00:00",
        "maxNumOfHikers": 100,
        "placeOfStart": "Mátrafüred, Mátra Szakképző Iskola (Erdész u. 11.)",
        "placeOfFinish": "Mátrafüred, Mátra Szakképző Iskola (Erdész u. 11.)",
        "distance": 43320,
        "elevation": 1922,
        "beginningOfStart": "2018-03-09T07:00:00",
        "endOfStart": "2018-03-09T08:00:00",
        "limitTime": "11:00:00",
        "checkPoints": null,
        "registrations": null,
        "participations": null
      }
    ]
  },
  {
    "id": 2,
    "name": "Patai Mátra",
    "date": "2018-07-22T00:00:00",
    "description": "Patai Mátra telejsítménytúra. Túra Gyöngyöspatáról 4 távon.",
    "website": "http://www.alfoldte.hu",
    "organizerId": 4,
    "organizer": null,
    "staff": null,
    "comments": null,
    "courses": [
      {
        "id": 2,
        "name": "Patai Mátra 50",
        "hikeId": 2,
        "price": 1600,
        "registerDeadline": "2018-07-21T00:00:00",
        "maxNumOfHikers": 60,
        "placeOfStart": "Gyöngyöspata, általános iskola",
        "placeOfFinish": "Gyöngyöspata, általános iskola",
        "distance": 51400,
        "elevation": 1992,
        "beginningOfStart": "2018-07-22T06:00:00",
        "endOfStart": "2018-07-22T08:00:00",
        "limitTime": "12:00:00",
        "checkPoints": null,
        "registrations": null,
        "participations": null
      },
      {
        "id": 3,
        "name": "Patai Mátra 35",
        "hikeId": 2,
        "price": 1400,
        "registerDeadline": "2018-07-21T00:00:00",
        "maxNumOfHikers": 100,
        "placeOfStart": "Gyöngyöspata, általános iskola",
        "placeOfFinish": "Gyöngyöspata, általános iskola",
        "distance": 34400,
        "elevation": 1338,
        "beginningOfStart": "2018-07-22T06:00:00",
        "endOfStart": "2018-07-22T08:00:00",
        "limitTime": "10:00:00",
        "checkPoints": null,
        "registrations": null,
        "participations": null
      }
    ]
  },
  {
    "id": 3,
    "name": "Andezit",
    "date": "2014-04-13T00:00:00",
    "description": "Andezit 30/15",
    "website": "http://www.bthe.hu",
    "organizerId": 5,
    "organizer": null,
    "staff": null,
    "comments": null,
    "courses": [
      {
        "id": 4,
        "name": "30 kilométeres táv.",
        "hikeId": 3,
        "price": 1200,
        "registerDeadline": "2014-04-10T00:00:00",
        "maxNumOfHikers": 40,
        "placeOfStart": "Galgaguta, evangélikus templom melletti park",
        "placeOfFinish": "Galgaguta, evangélikus templom melletti park",
        "distance": 29750,
        "elevation": 915,
        "beginningOfStart": "2014-04-13T07:30:00",
        "endOfStart": "2014-04-13T09:00:00",
        "limitTime": "08:00:00",
        "checkPoints": null,
        "registrations": null,
        "participations": null
      }
    ]
  }
]

export const input = {
  hikes: testHikes,
  text: "",
  sortBy: "date",
  startDate: null,
  endDate: null,
  isOldHikesVisible: true,
  slider: [0, 100],
}

