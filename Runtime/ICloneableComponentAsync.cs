using Cysharp.Threading.Tasks;

namespace Kogane
{
    /// <summary>
    /// Instantiate で複製して管理されるコンポーネントのインターフェイス（非同期版）
    /// </summary>
    public interface ICloneableComponentAsync<in TData>
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 表示を設定します
        /// </summary>
        UniTask SetupAsync( TData data );
    }
}