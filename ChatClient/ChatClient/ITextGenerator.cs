namespace ChatClient
{
    public interface ITextGenerator
    {
        /// <summary>
        /// Method for random name generating
        /// </summary>
        /// <returns>String name</returns>
        string GenerateName();

        /// <summary>
        /// Method for random message generating
        /// </summary>
        /// <returns>Message text</returns>
        string GenerateMessage();
    }
}
