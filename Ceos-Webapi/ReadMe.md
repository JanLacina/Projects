Service is run by compiling Ceos-WebApi project. Currently have one path api/books in HTTP GET method.

Testing you can do:
1) natively it opens in Swagger in which you can user see pagination. Further filtering I would do as DataSourceRequest or similar way in case of enormous data. At frontend in case of few data
2) by calling the localhost:xxx/api/books - as it is get, you cannot set anything special.
3) by using "Ceos-WebApi-TEST" project which use NUnit