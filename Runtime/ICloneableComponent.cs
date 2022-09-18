namespace Kogane
{
    /// <summary>
    /// Instantiate で複製して管理されるコンポーネントのインターフェイス
    /// </summary>
    public interface ICloneableComponent<in TData>
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 表示を設定します
        /// </summary>
        void Setup( TData data );
    }
}