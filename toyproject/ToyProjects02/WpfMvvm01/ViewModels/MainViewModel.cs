using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WpfMvvm01.Models;

namespace WpfMvvm01.ViewModels {

    // Observable(객체내용 변경 추적)
    // MainViewModel이 다른 클래스와 합쳐져서 컴파일됨 (pratial) 사용
    public partial class MainViewModel : ObservableObject {
        [ObservableProperty]//MVVM 에서 View가 바로 알아차리게 해주는 속성이라는 뜻
        private string message = "Hello Mvvm";   // Message 속성을 자동 생성

        [ObservableProperty]
        private Person? selectedPerson;

        public ObservableCollection<Person> People { get; } =
        [
            new Person {Name = "홍길동"},
            new Person {Name = "성유고"},
            new Person {Name = "애슐리"},
            new Person {Name = "김철수"}
        ];

        [RelayCommand]//View에서 넘어온 명령을 처리
        private void ChangeMessage()
        {
            Message = "버튼 클릭!!!!!";
        }
    }
}
