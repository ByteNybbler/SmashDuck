﻿// Author(s): Paul Calande
// A system of collections built for tracking claimable items.
// Keeps track of whether items are claimed or unclaimed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableElements<T>
{
    List<T> unclaimed = new List<T>();
    List<T> claimed = new List<T>();

    // Adds the given element to the unclaimed list.
    public void AddUnclaimed(T value)
    {
        unclaimed.Add(value);
    }

    // Removes the given element from the unclaimed list.
    public bool RemoveUnclaimed(T value)
    {
        return unclaimed.Remove(value);
    }

    // Adds the given element to the claimed list.
    public void AddClaimed(T value)
    {
        claimed.Add(value);
    }

    // Removes the given element from the claimed list.
    public bool RemoveClaimed(T value)
    {
        return claimed.Remove(value);
    }

    // Claim the given element.
    // This will move it from the unclaimed list to the claimed list.
    public void Claim(T value)
    {
        if (RemoveUnclaimed(value))
        {
            claimed.Add(value);
        }
    }

    // Unclaims the given element.
    // This will move it from the claimed list to the unclaimed list.
    public void Unclaim(T value)
    {
        if (RemoveClaimed(value))
        {
            unclaimed.Add(value);
        }
    }

    // Gets the number of unclaimed elements remaining.
    public int GetUnclaimedCount()
    {
        return unclaimed.Count;
    }

    // Gets the number of claimed elements.
    public int GetClaimedCount()
    {
        return claimed.Count;
    }

    // Returns a random element from the unclaimed list without removing it.
    public T GetRandomElementUnclaimed()
    {
        return UtilRandom.GetRandomElement(unclaimed);
    }

    // Returns a random element from the claimed list without removing it.
    public T GetRandomElementClaimed()
    {
        return UtilRandom.GetRandomElement(claimed);
    }

    // Returns a random collection of items from the unclaimed list without removing them.
    // The index of each item returned will be different.
    public List<T> GetRandomElementsUniqueUnclaimed(int count)
    {
        return UtilRandom.GetRandomElementsUnique(unclaimed, count);
    }

    // Same as the above method, but for the claim list.
    public List<T> GetRandomElementsUniqueClaimed(int count)
    {
        return UtilRandom.GetRandomElementsUnique(claimed, count);
    }

    // Returns the element with the given index in the claimed list.
    public T ClaimedAt(int index)
    {
        return claimed[index];
    }

    // Returns the element with the given index in the unclaimed list.
    public T UnclaimedAt(int index)
    {
        return unclaimed[index];
    }
}