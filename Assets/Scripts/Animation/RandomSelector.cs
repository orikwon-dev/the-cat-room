using UnityEngine;

namespace MyCat.Runtime.Animation
{
    public class RandomSelector : StateMachineBehaviour
    {
        // 무작위 재생을 위해 현재 서브 스테이트 머신의 클립 갯수를 설정한다
        [SerializeField] private int numberOfClips;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // TODO : (기술부채) 애니메이션 파라미터 이름을 직접 텍스트로 입력하는 동작을 방지하자.
            int randomVariant =  Random.Range(0, numberOfClips);
            animator.SetInteger("Variant", randomVariant);
        }
        
        // 임시 구현 - 별도의 클래스로 기능을 추가하자
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.GetBool("IsMoving"))
            {
                animator.SetTrigger("ExitState");
            }
        }
    }
}

