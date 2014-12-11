namespace ChickenProtector.Components
{
    #region Using statements

    using Artemis.Interface;

    #endregion

    /// <summary>The spatial form.</summary>
    public class AudioComponent : IComponent
    {
        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        public AudioComponent()
            : this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public AudioComponent(string audioFile)
        {
            this.AudioFile = audioFile;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public string AudioFile { get; set; }
    }
}