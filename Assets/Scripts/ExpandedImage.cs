using System.Collections.Generic;
using UnityEngine;

public class ExpandedImage : MonoBehaviour
{
    #region Variables

    [Header("Drag and drop all selected audio zone object onto this list using inspector lock")]
    [SerializeField] GameObject outskirtsPrefab; // Expanded image for Outskirts of Paris

    private bool outskirtsSpawned = false;
    private GameObject spawnedImage;

    #endregion
    #region Custom Methods

    ///  <summary>
    ///     Spawns expanded image for Outskirts of Paris
    /// </summary>

    public void spawnOutskirts()
    {
        if (!outskirtsSpawned)
        {
            spawnedImage = Instantiate(outskirtsPrefab, this.gameObject.transform.position, Quaternion.identity);
            outskirtsSpawned = true;
        }
    }

    public void removeSpawnedImage()
    {
        Destroy(spawnedImage);
    }

    #endregion
}
