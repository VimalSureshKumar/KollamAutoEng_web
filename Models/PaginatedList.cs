using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// A generic class that handles pagination for a list of items of type T
public class PaginatedList<T> : List<T>
{
    // Current page number
    public int PageIndex { get; private set; }

    // Total number of pages
    public int TotalPages { get; private set; }

    // Constructor that initializes the paginated list
    // Takes the list of items, the total count, the current page index, and the page size
    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        // Set the current page index
        PageIndex = pageIndex;

        // Calculate the total number of pages
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        // Add the provided items to the list
        this.AddRange(items);
    }

    // Property to check if there is a previous page
    public bool HasPreviousPage
    {
        get
        {
            // Returns true if the current page is greater than 1
            return (PageIndex > 1);
        }
    }

    // Property to check if there is a next page
    public bool HasNextPage
    {
        get
        {
            // Returns true if the current page is less than the total number of pages
            return (PageIndex < TotalPages);
        }
    }

    // Asynchronous method to create a paginated list from an IQueryable source
    // Takes the data source, page index, and page size as parameters
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        // Get the total count of items in the data source
        var count = await source.CountAsync();

        // Skip the items of previous pages and take only the items for the current page
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        // Return a new PaginatedList with the items and pagination details
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}
