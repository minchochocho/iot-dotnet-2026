/**
 * IoT 개발자 취업준비 마스터 시트 자동 생성
 * 사용법:
 * 1) Google 스프레드시트에서 확장 프로그램 > Apps Script 열기
 * 2) 이 파일 내용 전체 붙여넣기
 * 3) setupIotMasterSheet() 실행
 */

const MAX_ROWS = 1000;
const HEADER_BG = "#1F4E78";
const HEADER_FG = "#FFFFFF";
const LIGHT_BLUE = "#F8FBFF";

function setupIotMasterSheet() {
  const ss = SpreadsheetApp.getActiveSpreadsheet();
  const sheetNames = [
    "대시보드",
    "채용공고 분석",
    "직무역량 정리",
    "프로젝트 기술서",
    "자격증 및 공부계획",
    "자소서 재료 창고",
    "면접 질문 정리",
  ];

  // 기존 동일 시트 삭제 후 재생성
  sheetNames.forEach((name) => {
    const old = ss.getSheetByName(name);
    if (old) ss.deleteSheet(old);
  });

  const dashboard = ss.insertSheet("대시보드");
  const jobs = ss.insertSheet("채용공고 분석");
  const skills = ss.insertSheet("직무역량 정리");
  const projects = ss.insertSheet("프로젝트 기술서");
  const certs = ss.insertSheet("자격증 및 공부계획");
  const essay = ss.insertSheet("자소서 재료 창고");
  const interview = ss.insertSheet("면접 질문 정리");

  setupDashboard_(dashboard);
  setupJobs_(jobs);
  setupSkills_(skills);
  setupProjects_(projects);
  setupCerts_(certs);
  setupEssay_(essay);
  setupInterview_(interview);

  SpreadsheetApp.flush();
}

function setupDashboard_(sh) {
  sh.clear();
  sh.setHiddenGridlines(true);
  sh.setFrozenRows(2);

  setColumnWidths_(sh, [40, 230, 150, 230, 150, 40]);

  sh.getRange("B2").setValue("IoT 개발자 취업준비 대시보드").setFontSize(16).setFontWeight("bold").setFontColor("#1F4E78");

  const kpiRows = [
    ["조사한 기업 수", "=COUNTA('채용공고 분석'!A2:A)"],
    ["지원 예정 기업 수", '=COUNTIF(\'채용공고 분석\'!L2:L,"지원예정")'],
    ["지원 완료 기업 수", '=COUNTIF(\'채용공고 분석\'!L2:L,"지원완료")'],
    ["서류 합격 수", '=COUNTIF(\'채용공고 분석\'!L2:L,"서류합격")'],
    ["면접 진행 수", '=COUNTIF(\'채용공고 분석\'!L2:L,"면접예정")'],
    ["최종 합격 수", '=COUNTIF(\'채용공고 분석\'!L2:L,"최종합격")'],
  ];
  sh.getRange(4, 2, kpiRows.length, 2).setValues(kpiRows);

  const rightRows = [
    ["현재 보유 자격증 수", '=COUNTIF(\'자격증 및 공부계획\'!C2:C,"취득완료")'],
    ["진행 중 프로젝트 수", '=COUNTIF(\'프로젝트 기술서\'!P2:P,"X")'],
    ["자격증 평균 진행률", "=IFERROR(AVERAGE('자격증 및 공부계획'!F2:F),0)"],
  ];
  sh.getRange(4, 4, rightRows.length, 2).setValues(rightRows);
  sh.getRange("E6").setNumberFormat("0.0%");

  styleCardArea_(sh.getRange("B4:C9"));
  styleCardArea_(sh.getRange("D4:E6"));
  sh.getRange("B4:B9,D4:D6").setFontWeight("bold").setFontColor("#1F4E78");
  sh.getRange("C4:C9,E4:E6").setHorizontalAlignment("right");

  sh.getRange("B11").setValue("이번 주 할 일").setFontSize(12).setFontWeight("bold").setFontColor("#1F4E78");
  sh.getRange("B12:D12").setValues([["구분", "항목", "마감일"]]);
  styleHeader_(sh.getRange("B12:D12"));

  sh.getRange("B13").setFormula(
    '=IFERROR(SORT(FILTER({ARRAYFORMULA(IF(\'채용공고 분석\'!A2:A<>"","채용공고","")),\'채용공고 분석\'!A2:A,\'채용공고 분석\'!K2:K},(\'채용공고 분석\'!K2:K>=TODAY())*(\'채용공고 분석\'!K2:K<=TODAY()+7)*(\'채용공고 분석\'!L2:L<>"최종합격")*(\'채용공고 분석\'!L2:L<>"불합격")),3,TRUE),"")'
  );
  sh.getRange("B20").setValue("자격증/시험 일정(7일 내)").setFontWeight("bold").setFontColor("#1F4E78");
  sh.getRange("B21:D21").setValues([["구분", "항목", "시험일"]]);
  styleHeader_(sh.getRange("B21:D21"));
  sh.getRange("B22").setFormula(
    '=IFERROR(SORT(FILTER({ARRAYFORMULA(IF(\'자격증 및 공부계획\'!B2:B<>"","자격증","")),\'자격증 및 공부계획\'!B2:B,\'자격증 및 공부계획\'!D2:D},(\'자격증 및 공부계획\'!D2:D>=TODAY())*(\'자격증 및 공부계획\'!D2:D<=TODAY()+7)*(\'자격증 및 공부계획\'!C2:C<>"취득완료")),3,TRUE),"")'
  );

  sh.getRange("B13:D19").setBorder(true, true, true, true, true, true, "#D9D9D9", SpreadsheetApp.BorderStyle.SOLID);
  sh.getRange("B22:D28").setBorder(true, true, true, true, true, true, "#D9D9D9", SpreadsheetApp.BorderStyle.SOLID);
  sh.getRange("B13:D28").setWrap(true);

  sh.getRange("B30").setValue("권장 흐름: 채용공고 분석 → 직무역량 정리 → 프로젝트 기술서 → 자소서 작성 → 면접 준비").setFontStyle("italic").setFontColor("#666666");
}

function setupJobs_(sh) {
  const headers = [
    "기업명",
    "산업",
    "기업규모",
    "지역",
    "직무",
    "연봉",
    "필수조건",
    "우대사항",
    "기술스택",
    "채용공고 링크",
    "마감일",
    "지원상태",
    "관심도",
    "메모",
  ];
  initializeTableSheet_(sh, headers, [130, 90, 90, 90, 110, 90, 220, 220, 150, 220, 95, 95, 80, 180]);

  applyDropdown_(sh, "L2:L1000", ["조사중", "지원예정", "지원완료", "서류합격", "면접예정", "최종합격", "불합격"]);
  applyDropdown_(sh, "M2:M1000", ["★★★★★", "★★★★", "★★★", "★★", "★"]);

  sh.getRange("K2:K1000").setNumberFormat("yyyy-mm-dd");

  const range = sh.getRange("A2:N1000");
  const rules = sh.getConditionalFormatRules();

  rules.push(
    SpreadsheetApp.newConditionalFormatRule()
      .whenFormulaSatisfied('=$L2="지원완료"')
      .setBackground("#D9E1F2")
      .setRanges([range])
      .build()
  );
  rules.push(
    SpreadsheetApp.newConditionalFormatRule()
      .whenFormulaSatisfied('=$L2="최종합격"')
      .setBackground("#D9EAD3")
      .setRanges([range])
      .build()
  );
  rules.push(
    SpreadsheetApp.newConditionalFormatRule()
      .whenFormulaSatisfied('=AND($K2>=TODAY(),$K2<=TODAY()+7,$L2<>"최종합격",$L2<>"불합격")')
      .setBackground("#F4CCCC")
      .setRanges([range])
      .build()
  );
  sh.setConditionalFormatRules(rules);
}

function setupSkills_(sh) {
  const headers = ["구분", "항목명", "설명", "관련기술", "증빙자료", "직무연관성", "중요도", "비고"];
  initializeTableSheet_(sh, headers, [80, 150, 240, 150, 140, 110, 80, 180]);

  applyDropdown_(sh, "A2:A1000", ["교육", "자격증", "기술", "프로젝트", "활동"]);
  applyDropdown_(sh, "F2:F1000", ["매우높음", "높음", "보통", "낮음"]);

  const validation = SpreadsheetApp.newDataValidation().requireNumberBetween(1, 5).setAllowInvalid(false).build();
  sh.getRange("G2:G1000").setDataValidation(validation);
}

function setupProjects_(sh) {
  const headers = [
    "프로젝트명",
    "기간",
    "인원",
    "프로젝트 배경",
    "프로젝트 목적",
    "사용 기술",
    "시스템 구조",
    "나의 역할",
    "주요 기능",
    "문제점",
    "해결 방법",
    "결과",
    "배운 점",
    "직무연관성",
    "Github 링크",
    "포트폴리오 반영 여부",
  ];
  initializeTableSheet_(sh, headers, [130, 95, 65, 190, 190, 130, 160, 140, 140, 160, 160, 130, 130, 100, 210, 120]);

  applyDropdown_(sh, "P2:P1000", ["O", "X"]);
  sh.getRange("B2:B1000").setNumberFormat("yyyy-mm-dd");
}

function setupCerts_(sh) {
  const headers = ["구분", "목표", "현재상태", "시험일", "우선순위", "진행률", "예상완료일", "비고"];
  initializeTableSheet_(sh, headers, [90, 160, 110, 95, 90, 80, 95, 180]);

  applyDropdown_(sh, "C2:C1000", ["예정", "공부중", "접수완료", "취득완료"]);
  applyDropdown_(sh, "E2:E1000", ["★★★★★", "★★★★", "★★★", "★★", "★"]);

  const pValidation = SpreadsheetApp.newDataValidation().requireNumberBetween(0, 1).setAllowInvalid(false).build();
  sh.getRange("F2:F1000").setDataValidation(pValidation);
  sh.getRange("F2:F1000").setNumberFormat("0%");
  sh.getRange("D2:D1000,G2:G1000").setNumberFormat("yyyy-mm-dd");

  const target = sh.getRange("F2:F1000");
  const rules = sh.getConditionalFormatRules();
  rules.push(
    SpreadsheetApp.newConditionalFormatRule()
      .whenNumberEqualTo(1)
      .setBackground("#D9EAD3")
      .setRanges([target])
      .build()
  );
  rules.push(
    SpreadsheetApp.newConditionalFormatRule()
      .whenFormulaSatisfied("=AND($F2>=0.5,$F2<1)")
      .setBackground("#FFF2CC")
      .setRanges([target])
      .build()
  );
  rules.push(
    SpreadsheetApp.newConditionalFormatRule()
      .whenFormulaSatisfied("=AND($F2>=0,$F2<0.5)")
      .setBackground("#F4CCCC")
      .setRanges([target])
      .build()
  );
  sh.setConditionalFormatRules(rules);
}

function setupEssay_(sh) {
  const headers = ["경험명", "구분", "상황", "문제", "해결", "결과", "활용가능문항", "키워드"];
  initializeTableSheet_(sh, headers, [140, 100, 210, 210, 210, 170, 210, 170]);

  // 샘플 1행
  sh.getRange("A2:H2").setValues([[
    "졸업작품",
    "IoT 과정",
    "스마트홈 센서 대시보드를 제작함",
    "실시간 데이터 반영 지연 발생",
    "MQTT + 비동기 처리로 병목 구간 개선",
    "응답속도 30% 개선",
    "경험 및 프로젝트",
    "IoT, WPF, MQTT",
  ]]);
}

function setupInterview_(sh) {
  const headers = ["구분", "질문", "답변", "완성도", "최종수정일"];
  initializeTableSheet_(sh, headers, [90, 280, 360, 110, 100]);

  applyDropdown_(sh, "A2:A1000", ["기업", "직무", "프로젝트", "인성"]);
  applyDropdown_(sh, "D2:D1000", ["미작성", "초안", "작성완료", "암기완료"]);
  sh.getRange("E2:E1000").setNumberFormat("yyyy-mm-dd");
}

function initializeTableSheet_(sh, headers, widths) {
  sh.clear();
  sh.setFrozenRows(1);
  sh.getRange(1, 1, 1, headers.length).setValues([headers]);
  styleHeader_(sh.getRange(1, 1, 1, headers.length));

  for (let i = 0; i < widths.length; i++) {
    sh.setColumnWidth(i + 1, widths[i]);
  }

  sh.getRange(2, 1, MAX_ROWS - 1, headers.length)
    .setFontFamily("Malgun Gothic")
    .setFontSize(10)
    .setVerticalAlignment("top")
    .setWrap(true)
    .setBackground(LIGHT_BLUE);

  sh.getRange(1, 1, MAX_ROWS, headers.length)
    .setBorder(true, true, true, true, true, true, "#D9D9D9", SpreadsheetApp.BorderStyle.SOLID);

  // 필터 + 기본 정렬 기준 컬럼(마지막에서 3번째 날짜 컬럼이 있으면 사용)
  const existingFilter = sh.getFilter();
  if (existingFilter) existingFilter.remove();
  sh.getRange(1, 1, MAX_ROWS, headers.length).createFilter();
}

function styleHeader_(range) {
  range
    .setBackground(HEADER_BG)
    .setFontColor(HEADER_FG)
    .setFontWeight("bold")
    .setFontFamily("Malgun Gothic")
    .setHorizontalAlignment("center")
    .setVerticalAlignment("middle")
    .setWrap(true);
}

function styleCardArea_(range) {
  range
    .setBackground("#F8FBFF")
    .setBorder(true, true, true, true, true, true, "#D9D9D9", SpreadsheetApp.BorderStyle.SOLID)
    .setFontFamily("Malgun Gothic")
    .setFontSize(10)
    .setVerticalAlignment("middle");
}

function applyDropdown_(sh, a1, values) {
  const rule = SpreadsheetApp.newDataValidation().requireValueInList(values, true).setAllowInvalid(false).build();
  sh.getRange(a1).setDataValidation(rule);
}

/**
 * 선택사항: 마감일/시험일이 수정되면 해당 시트를 자동 정렬
 */
function onEdit(e) {
  if (!e || !e.range) return;
  const sh = e.range.getSheet();
  const name = sh.getName();
  const row = e.range.getRow();
  if (row <= 1) return;

  if (name === "채용공고 분석") {
    // K열(마감일) 오름차순
    sh.getRange(2, 1, MAX_ROWS - 1, 14).sort([{ column: 11, ascending: true }]);
  } else if (name === "자격증 및 공부계획") {
    // D열(시험일) 오름차순
    sh.getRange(2, 1, MAX_ROWS - 1, 8).sort([{ column: 4, ascending: true }]);
  }
}
