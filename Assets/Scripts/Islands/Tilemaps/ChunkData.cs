using System;
using System.Collections.Generic;
using System.Linq;
using Pandawan.Islands.Other;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pandawan.Islands.Tilemaps
{
    [Serializable]
    public enum ChunkDataType
    {
        Integer,
        String,
        Float,
        Double,
        UnityObject,
        Color
    }

    [Serializable]
    public class ChunkData
    {
        [SerializeField] private readonly Dictionary<ChunkDataKey, ChunkDataValue> positionProperties =
            new Dictionary<ChunkDataKey, ChunkDataValue>();

        // Size of a chunk
        [NonSerialized] private readonly BoundsInt chunkBounds;

        internal Dictionary<ChunkDataKey, ChunkDataValue> PositionProperties => positionProperties;

        #region Constructor

        public ChunkData(BoundsInt chunkBounds)
        {
            this.chunkBounds = chunkBounds;
        }

        #endregion

        #region Set

        public bool SetPositionProperty<T>(Vector3Int globalPosition, string name, T positionProperty)
        {
            throw new NotImplementedException("Storing this type is not accepted in ChunkData");
        }

        public bool SetPositionProperty(Vector3Int globalPosition, string name, int positionProperty)
        {
            return SetPositionProperty(globalPosition, name, ChunkDataType.Integer, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int globalPosition, string name, string positionProperty)
        {
            return SetPositionProperty(globalPosition, name, ChunkDataType.String, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int globalPosition, string name, float positionProperty)
        {
            return SetPositionProperty(globalPosition, name, ChunkDataType.Float, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int globalPosition, string name, double positionProperty)
        {
            return SetPositionProperty(globalPosition, name, ChunkDataType.Double, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int globalPosition, string name, Object positionProperty)
        {
            return SetPositionProperty(globalPosition, name, ChunkDataType.UnityObject, positionProperty);
        }

        public bool SetPositionProperty(Vector3Int globalPosition, string name, Color positionProperty)
        {
            return SetPositionProperty(globalPosition, name, ChunkDataType.Color, positionProperty);
        }

        private bool SetPositionProperty(Vector3Int globalPosition, string name, ChunkDataType dataType,
            object positionProperty)
        {
            if (positionProperty != null)
            {
                Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

                ChunkDataKey positionKey;
                positionKey.position = localPosition;
                positionKey.name = name;

                ChunkDataValue positionValue;
                positionValue.type = dataType;
                positionValue.data = positionProperty;

                positionProperties[positionKey] = positionValue;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Remove the property at the given position and with the given name.
        /// </summary>
        /// <param name="globalPosition">The position of the property to erase.</param>
        /// <param name="name">The name of the property to erase.</param>
        public bool ErasePositionProperty(Vector3Int globalPosition, string name)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            ChunkDataKey positionKey;
            positionKey.position = localPosition;
            positionKey.name = name;
            return positionProperties.Remove(positionKey);
        }

        /// <summary>
        ///     Remove all of the properties at the given position (regardless of their name).
        /// </summary>
        /// <param name="globalPosition">The position of the property to erase.</param>
        public bool ErasePositionProperty(Vector3Int globalPosition)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);
            return positionProperties.RemoveAll((key, value) => key.position == localPosition);
        }

        #endregion

        #region Get

        public T GetPositionProperty<T>(Vector3Int globalPosition, string name, T defaultValue) where T : Object
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            ChunkDataKey positionKey;
            positionKey.position = localPosition;
            positionKey.name = name;

            ChunkDataValue positionValue;
            if (positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != ChunkDataType.UnityObject)
                    throw new InvalidCastException("Value stored in ChunkData is not of the right type");
                return positionValue.data as T;
            }

            return defaultValue;
        }

        public int GetPositionProperty(Vector3Int globalPosition, string name, int defaultValue)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            ChunkDataKey positionKey;
            positionKey.position = localPosition;
            positionKey.name = name;

            ChunkDataValue positionValue;
            if (positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != ChunkDataType.Integer)
                    throw new InvalidCastException("Value stored in ChunkData is not of the right type");
                return (int) positionValue.data;
            }

            return defaultValue;
        }

        public string GetPositionProperty(Vector3Int globalPosition, string name, string defaultValue)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            ChunkDataKey positionKey;
            positionKey.position = localPosition;
            positionKey.name = name;

            ChunkDataValue positionValue;
            if (positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != ChunkDataType.String)
                    throw new InvalidCastException("Value stored in ChunkData is not of the right type");
                return (string) positionValue.data;
            }

            return defaultValue;
        }

        public float GetPositionProperty(Vector3Int globalPosition, string name, float defaultValue)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            ChunkDataKey positionKey;
            positionKey.position = localPosition;
            positionKey.name = name;

            ChunkDataValue positionValue;
            if (positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != ChunkDataType.Float)
                    throw new InvalidCastException("Value stored in ChunkData is not of the right type");
                return (float) positionValue.data;
            }

            return defaultValue;
        }

        public double GetPositionProperty(Vector3Int globalPosition, string name, double defaultValue)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            ChunkDataKey positionKey;
            positionKey.position = localPosition;
            positionKey.name = name;

            ChunkDataValue positionValue;
            if (positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != ChunkDataType.Double)
                    throw new InvalidCastException("Value stored in ChunkData is not of the right type");
                return (double) positionValue.data;
            }

            return defaultValue;
        }

        public Color GetPositionProperty(Vector3Int globalPosition, string name, Color defaultValue)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            ChunkDataKey positionKey;
            positionKey.position = localPosition;
            positionKey.name = name;

            ChunkDataValue positionValue;
            if (positionProperties.TryGetValue(positionKey, out positionValue))
            {
                if (positionValue.type != ChunkDataType.Color)
                    throw new InvalidCastException("Value stored in ChunkData is not of the right type");
                return (Color) positionValue.data;
            }

            return defaultValue;
        }

        /// <summary>
        ///     Get a dictionary with all the properties at that position.
        /// </summary>
        /// <param name="globalPosition">The position at which to get the properties.</param>
        public Dictionary<ChunkDataKey, ChunkDataValue> GetAllPropertiesAt(Vector3Int globalPosition)
        {
            Vector3Int localPosition = GlobalToLocalPosition(globalPosition);

            return positionProperties.Keys.ToList().FindAll(x => x.position == localPosition)
                .ToDictionary(key => key, key => positionProperties[key]);
        }

        /// <summary>
        ///     Get a list of all positions that contain this property.
        /// </summary>
        /// <param name="propertyName">The property to search for.</param>
        public Vector3Int[] GetAllPositionsWithProperty(string propertyName)
        {
            return positionProperties.Keys.ToList().FindAll(x => x.name == propertyName).Select(x => x.position)
                .ToArray();
        }

        #endregion

        #region Others

        /// <summary>
        ///     Clear every value in the ChunkData
        /// </summary>
        public virtual void Reset()
        {
            positionProperties.Clear();
        }

        /// <summary>
        ///     Convert a World/Tile Position to a Local/Chunk Position
        /// </summary>
        /// <param name="globalPosition">The Global Position to convert.</param>
        /// <returns>The converted Local Position.</returns>
        public Vector3Int GlobalToLocalPosition(Vector3Int globalPosition)
        {
            // Check that this globalPosition is valid for this ChunkData
            if (!IsValidPosition(globalPosition))
                Debug.LogError($"Position {globalPosition} is not valid for Chunk at {chunkBounds.position}");

            return new Vector3Int((globalPosition.x % chunkBounds.size.x + chunkBounds.size.x) % chunkBounds.size.x,
                (globalPosition.y % chunkBounds.size.y + chunkBounds.size.y) % chunkBounds.size.y,
                (globalPosition.z % chunkBounds.size.z + chunkBounds.size.z) % chunkBounds.size.z);
        }


        /// <summary>
        ///     Whether or not the given Tile Position is valid in this chunk.
        /// </summary>
        /// <param name="globalPosition">The position of the tile to check.</param>
        /// <returns>True if it is valid in this chunk.</returns>
        private bool IsValidPosition(Vector3Int globalPosition)
        {
            // Use the World's formula for chunk positions and check that they match
            return World.instance.GetChunkPositionForTile(globalPosition) == chunkBounds.position;
        }

        #endregion

        #region Structs

        [Serializable]
        public struct ChunkDataValue
        {
            public ChunkDataType type;
            public object data;

            public override string ToString()
            {
                return $"{{ type: {type}, data: {data} }}";
            }
        }

        [Serializable]
        public struct ChunkDataKey
        {
            public Vector3Int position;
            public string name;

            public override string ToString()
            {
                return $"{{ position: {position}, name: {name} }}";
            }
        }

        #endregion
    }
}