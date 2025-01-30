* README

[[file:~/llm/2025-01-28t13:18:33-gptel-7be8328bfb37265.md::Here's a csv listing departments and child departments:]]

** Library
*** CsvHelper
dotnet add package CsvHelper

** Input
OID,Title,Color,DepartmentParent_OID
1,US News,#F52612,
2,Crime + Justice,#F52612,1
3,Energy + Environment,#F52612,1
4,Extreme Weather,#F52612,1
5,Space + Science,#F52612,1
6,International News,#EB5F25,
7,Africa,#EB5F25,6
8,Americas,#EB5F25,6
9,Asia,#EB5F25,6
10,Europe,#EB5F25,6

** Requirements
*** DONE The number of descendants is not only the number of children, but the number of children, the childrens children and so on.
*** DONE API using ASP.NET Core
*** DONE output the department hierarchy as a JSON document
*** DONE For each node give a count of the descendants.
*** DONE Read the file from disk
*** DONE Should support any depth in the department hierarchy structure. 
*** DONE Should have tests
*** DONE Should give a message if parsing of the file fails

** API Output
*** Flat
#+begin_src shell :results verbatim
curl -k -X 'GET' \
  'https://localhost:7052/Claims' \
  -H 'accept: application/json' | jq .
#+end_src

#+RESULTS:
#+begin_example
[
  {
    "id": 1,
    "title": "US News",
    "color": "#F52612",
    "departmentParent_OID": null,
    "children": []
  },
  {
    "id": 2,
    "title": "Crime + Justice",
    "color": "#F52612",
    "departmentParent_OID": 1,
    "children": []
  },
  {
    "id": 3,
    "title": "Energy + Environment",
    "color": "#F52612",
    "departmentParent_OID": 1,
    "children": []
  },
  {
    "id": 4,
    "title": "Extreme Weather",
    "color": "#F52612",
    "departmentParent_OID": 1,
    "children": []
  },
  {
    "id": 5,
    "title": "Space + Science",
    "color": "#F52612",
    "departmentParent_OID": 1,
    "children": []
  },
  {
    "id": 6,
    "title": "International News",
    "color": "#EB5F25",
    "departmentParent_OID": null,
    "children": []
  },
  {
    "id": 7,
    "title": "Africa",
    "color": "#EB5F25",
    "departmentParent_OID": 6,
    "children": []
  },
  {
    "id": 8,
    "title": "Americas",
    "color": "#EB5F25",
    "departmentParent_OID": 6,
    "children": []
  },
  {
    "id": 9,
    "title": "Asia",
    "color": "#EB5F25",
    "departmentParent_OID": 6,
    "children": []
  },
  {
    "id": 10,
    "title": "Europe",
    "color": "#EB5F25",
    "departmentParent_OID": 6,
    "children": []
  }
]
#+end_example
*** hierarchy
#+begin_src shell :results verbatim
curl -k -X 'GET' \
  'https://localhost:7052/Claims/hierarchy' \
  -H 'accept: application/json' | jq .
#+end_src

#+RESULTS:
#+begin_example
[
  {
    "id": 1,
    "title": "US News",
    "color": "#F52612",
    "departmentParent_OID": null,
    "numDescendants": 4,
    "children": [
      {
        "id": 2,
        "title": "Crime + Justice",
        "color": "#F52612",
        "departmentParent_OID": 1,
        "numDescendants": 0,
        "children": []
      },
      {
        "id": 3,
        "title": "Energy + Environment",
        "color": "#F52612",
        "departmentParent_OID": 1,
        "numDescendants": 0,
        "children": []
      },
      {
        "id": 4,
        "title": "Extreme Weather",
        "color": "#F52612",
        "departmentParent_OID": 1,
        "numDescendants": 0,
        "children": []
      },
      {
        "id": 5,
        "title": "Space + Science",
        "color": "#F52612",
        "departmentParent_OID": 1,
        "numDescendants": 0,
        "children": []
      }
    ]
  },
  {
    "id": 6,
    "title": "International News",
    "color": "#EB5F25",
    "departmentParent_OID": null,
    "numDescendants": 4,
    "children": [
      {
        "id": 7,
        "title": "Africa",
        "color": "#EB5F25",
        "departmentParent_OID": 6,
        "numDescendants": 0,
        "children": []
      },
      {
        "id": 8,
        "title": "Americas",
        "color": "#EB5F25",
        "departmentParent_OID": 6,
        "numDescendants": 0,
        "children": []
      },
      {
        "id": 9,
        "title": "Asia",
        "color": "#EB5F25",
        "departmentParent_OID": 6,
        "numDescendants": 0,
        "children": []
      },
      {
        "id": 10,
        "title": "Europe",
        "color": "#EB5F25",
        "departmentParent_OID": 6,
        "numDescendants": 0,
        "children": []
      }
    ]
  }
]
#+end_example


** Tests
Passed!  - Failed:     0, Passed:     5, Skipped:     0, Total:     5, Duration: 1 s - Claims.Tests.dll (net8.0)
