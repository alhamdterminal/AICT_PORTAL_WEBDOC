
    var data = [];
    $(function () {
        $.get('/APICalls/GetDashboardItemExport', function (res) {


            data = res;

            console.log("TEst data", data)

            //       $("#chart").dxChart({
            //    palette: "Harmony Light",
            //    dataSource: data,
            //    title: " ",

            //    valueAxis: [{
            //        name: "frequency",
            //        position: "left",
            //        tickInterval: 300
            //    }],
            //    commonSeriesSettings: {
            //        argumentField: "complaint"
            //    },
            //    series: [{
            //        type: "bar",
            //        valueField: "count",
            //        axis: "frequency",
            //        name: "Complaint frequency",
            //        color: "#fac29a"
            //    }],
            //    legend: {
            //        verticalAlignment: "top",
            //        horizontalAlignment: "center"
            //    }
            //});
            let TotalCargoReceived = data.find(x => x.complaint == "TotalCargoReceived");
            let CargoReceivedCBM = data.find(x => x.complaint == "CargoReceivedCBM");
            let TotalContainerGateOut = data.find(x => x.complaint == "TotalContainerGateOut");
            let EmptyContainerReceiving = data.find(x => x.complaint == "EmptyContainerReceiving");
            let UpcomingGDsCount = data.find(x => x.complaint == "UpcomingGDsCount");
            let WaitingforExaminationMarkedGDs = data.find(x => x.complaint == "WaitingforExaminationMarkedGDs");
            let GroundedGDs = data.find(x => x.complaint == "GroundedGDs");
            let ContainerReadyForGateOut = data.find(x => x.complaint == "ContainerReadyForGateOut");
            let CurrentGDHold = data.find(x => x.complaint == "CurrentGDHold");
            let CutterntGateOutContainer = data.find(x => x.complaint == "CutterntGateOutContainer");
            let TotalDrayOFFGDs = data.find(x => x.complaint == "TotalDrayOFFGDs");


            $('#totalCargoReceived').html(TotalCargoReceived.count);
            $('#cargoReceivedCBM').html(CargoReceivedCBM.count);
            $('#totalContainerGateOut').html(TotalContainerGateOut.count);
            $('#emptyContainerReceiving').html(EmptyContainerReceiving.count);
            $('#upcomingGDsCount').html(UpcomingGDsCount.count);
            $('#waitingforExaminationMarkedGDs').html(WaitingforExaminationMarkedGDs.count);
            $('#groundedGDs').html(GroundedGDs.count);
            $('#containerReadyForGateOut').html(ContainerReadyForGateOut.count);
            $('#currentGDHold').html(CurrentGDHold.count);
            $('#cutterntGateOutContainer').html(CutterntGateOutContainer.count);
            $('#totalDrayOFFGDs').html(TotalDrayOFFGDs.count);

        })

        console.log("data", data)


       
    });
