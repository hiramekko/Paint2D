using UnityEngine;

/// <summary>
/// 左クリックを押してドラッグすると線を引ける
/// </summary>
public class PaintController : MonoBehaviour
{
    [Tooltip("線のプレハブ")]
    [SerializeField] GameObject _linePrefab;
    [Tooltip("線が出力されるまでの長さ")]
    [SerializeField] float _length = 0.1f;
    [Tooltip("線の大きさ")]
    [SerializeField] float _width = 0.1f;
    Vector2 _startPos;//線の起点
    Vector2 _endPos;//線の終点

    void Update()
    {
        Draw();
    }

    void Draw()
    {
        // 左クリック押した時
        if (Input.GetMouseButtonDown(0))
        {
            //線の起点をマウスを押した位置にする
            _startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //ドラッグしなければ点にする
            //InstantiateしたオブジェクトはObject型で返されるのでGameObjectに変更
            GameObject gm = Instantiate(_linePrefab, _startPos, Quaternion.identity) as GameObject;
        }
        //左クリックを押してる間
        else if (Input.GetMouseButton(0))
        {
            _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //ドラッグした時の長さが_lengthを超えたら線が出力される
            if ((_endPos - _startPos).magnitude > _length)
            {
                GameObject gm = Instantiate(_linePrefab, transform.position, Quaternion.identity) as GameObject;
                gm.transform.position = (_startPos + _endPos) / 2;
                gm.transform.right = (_endPos - _startPos).normalized;//正規化
                gm.transform.localScale = new Vector2((_endPos - _startPos).magnitude, _width);
                _startPos = _endPos;
            }
        }
    }
}